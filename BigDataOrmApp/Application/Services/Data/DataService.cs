using BigDataOrmApp.Application.Services.BigData;
using BigDataOrmApp.Domain;
using BigDataOrmApp.Domain.BigData;
using BigDataOrmApp.Infrastructure;

namespace BigDataOrmApp.Application.Services.Data;

public class DataService : IDataService
{
    private readonly DataDbContext _dbContext;
    private readonly IBigDataService _bigDataService;

    public DataService(DataDbContext dbContext, IBigDataService bigDataService)
    {
        _dbContext = dbContext;
        _bigDataService = bigDataService;
    }

    public async Task<double> CountDocumentsContainingWord(string word)
    {
        var result = await _bigDataService.CountDocumentsContainingWord(word);
        return result;
    }

    public async Task<List<TextDocument>> GetDocumentsByWord(string word)
    {
        var result = await _bigDataService.GetDocumentsByWord(word);
        return result;
    }

    public async Task AddNewData(string data)
    {
        var doc = new TextDocumentEntity();
        _dbContext.TextDocuments.Add(doc);
        await _dbContext.SaveChangesAsync();

        await _bigDataService.AddNewContent(data, doc.Id);
    }

    public async Task DeleteData(int id)
    {

        var data = await _dbContext.TextDocuments.FindAsync(id);
        if (data is not null)
        {
            await _bigDataService.DeleteDocument(id);
            _dbContext.TextDocuments.Remove(data);
            await _dbContext.SaveChangesAsync();
        }
    }
}