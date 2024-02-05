using BigDataOrmApp.Domain.BigData;

namespace BigDataOrmApp.Application.Services.BigData;

public interface IBigDataService
{
    Task<List<TextDocument>> GetDocumentsByWord(string word);
    Task AddNewContent(string data, int id);
    Task DeleteDocument(int id);
    Task<long> CountDocumentsContainingWord(string word);
}