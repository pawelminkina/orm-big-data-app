using BigDataOrmApp.Application.Models;
using BigDataOrmApp.Application.Services.Data;
using BigDataOrmApp.Domain.BigData;
using Microsoft.AspNetCore.Mvc;

namespace BigDataOrmApp.Controllers;

[ApiController]
[Route("[controller]")]
public class DataController : ControllerBase
{
    private readonly IDataService _dataService;

    public DataController(IDataService dataService)
    {
        _dataService = dataService;
    }

    [HttpGet("CountDocumentsContainingWord")]
    public async Task<double> GetWordCount(string word)
    {
        return await _dataService.CountDocumentsContainingWord(word);
    }

    [HttpGet("GetDocumentsByWord")]
    public async Task<List<TextDocument>> GetDocumentsByWord(string word)
    {
        return await _dataService.GetDocumentsByWord(word);
    }

    [HttpPost(Name = "AddNewData")]
    public async Task<ActionResult> AddNewData([FromBody] AddData data)
    {
        await _dataService.AddNewData(data.Data);
        return NoContent();
    }

    [HttpDelete(Name = "DeleteData")]
    public async Task<ActionResult> DeleteData(int id)
    {
        await _dataService.DeleteData(id);
        return NoContent();
    }
}