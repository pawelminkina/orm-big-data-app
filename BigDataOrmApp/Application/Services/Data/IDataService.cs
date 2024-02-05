using BigDataOrmApp.Domain.BigData;

namespace BigDataOrmApp.Application.Services.Data;

public interface IDataService
{
    Task<List<TextDocument>> GetDocumentsByWord(string word);
    Task<double> CountDocumentsContainingWord(string word);
    Task AddNewData(string data);
    Task DeleteData(int id);
}