using BigDataOrmApp.Domain.BigData;
using Nest;
using System.Reflection.Metadata;
using BigDataOrmApp.Infrastructure.Config;
using Microsoft.Extensions.Options;


namespace BigDataOrmApp.Application.Services.BigData;

public class BigDataService : IBigDataService
{
    private readonly ElasticClient _elasticClient;
    private readonly string _index;

    public BigDataService(ILogger<BigDataService> logger, IOptions<ElasticSearchConfig> options)
    {
        var optionsValue = options.Value;
        var defaultNodeUri = new Uri(optionsValue.Url);
        _index = optionsValue.DefaultIndex;

        var settings = new ConnectionSettings(defaultNodeUri)
            .EnableApiVersioningHeader()
            .BasicAuthentication(optionsValue.UserName, optionsValue.UserPassword)
            .CertificateFingerprint(optionsValue.CertFingerprint)
            .DefaultIndex(_index);


        _elasticClient = new ElasticClient(settings);
    }
    public async Task AddNewContent(string data, int id)
    {
        var createIndexResponse = await _elasticClient.Indices.CreateAsync("my_index", c => c
            .Map<TextDocument>(m => m
                .Properties(p => p
                    .Text(t => t
                        .Name(n => n.Content)
                        .Fielddata(true) 
                    )
                )
            )
        );

        var document = new TextDocument
        {
            Id = id,
            Content = data
        };
        var indexResponse = await _elasticClient.IndexDocumentAsync(document);

        if (!indexResponse.IsValid)
        {
            // Handle the error
            throw new System.Exception("Failed to index document: " + indexResponse.OriginalException.Message);
        }
    }

    public async Task<List<TextDocument>> GetDocumentsByWord(string word)
    {
        var response = await _elasticClient.SearchAsync<TextDocument>(s => s
            .Query(q => q
                .Match(m => m
                    .Field(f => f.Content)
                    .Query(word)
                )
            )
        );


        if (!response.IsValid || response.Documents == null)
        {
            throw new Exception("Query failed to execute or returned no results.");
        }

        return response.Documents.ToList();
    }

    public async Task<long> CountDocumentsContainingWord(string word)
    {
        var response = await _elasticClient.CountAsync<TextDocument>(s => s
            .Query(q => q
                .Match(m => m
                    .Field(f => f.Content)
                    .Query(word)
                )
            )
        );

        return response.Count;
    }

    public async Task DeleteDocument(int id)
    {
        var response = await _elasticClient.DeleteAsync(new DeleteRequest(_index, id));

        if (!response.IsValid)
        {
            // Handle error
            throw new Exception($"Failed to delete document with ID {id}: {response.OriginalException.Message}");
        }
    }
}