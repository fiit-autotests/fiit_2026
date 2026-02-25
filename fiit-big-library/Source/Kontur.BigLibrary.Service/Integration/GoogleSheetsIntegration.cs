using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using System.Collections.Generic;
using System.Linq;

public class GoogleSheetsIntegration
{
    private SheetsService _sheetsService;

    public GoogleSheetsIntegration()
    {
        // Путь к JSON-файлу с ключами доступа
        string keyFilePath = "Integration/secrets.json";

        // Создание учетных данных и аутентификация
        var credential = GoogleCredential.FromFile(keyFilePath)
            .CreateScoped(SheetsService.Scope.Spreadsheets);

        // Создание клиента Google Sheets API
        _sheetsService = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential
        });
    }

    public IList<IList<object>> ReadData(string spreadsheetId, string range)
    {
        // Выполнение запроса на чтение данных
        SpreadsheetsResource.ValuesResource.GetRequest request =
            _sheetsService.Spreadsheets.Values.Get(spreadsheetId, range);

        ValueRange response = request.Execute();
        IList<IList<object>> values = response.Values;

        return values;
    }
    
    public void DeleteSheet(string spreadsheetId, int sheetId)
    {
        BatchUpdateSpreadsheetRequest batchUpdateSpreadsheetRequest = new BatchUpdateSpreadsheetRequest();
        batchUpdateSpreadsheetRequest.Requests = new List<Request>
        {
            new Request
            {
                DeleteSheet = new DeleteSheetRequest()
                {
                    SheetId = sheetId
                }
            }
        };
    
        var request =
            _sheetsService.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, spreadsheetId);
        request.Execute();
    }
    
    public void AddSheet(string spreadsheetId, string title)
    {
        var batchUpdateSpreadsheetRequest = new BatchUpdateSpreadsheetRequest
        {
            Requests = new List<Request>
            {
                new()
                {
                    AddSheet = new AddSheetRequest()
                    {
                        Properties = new SheetProperties()
                        {
                            Title = title
                        }
                    }
                }
            }
        };

        var request =
            _sheetsService.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, spreadsheetId);
        request.Execute();
    }

    public void AddData(ValueRange valueDataRange, string spreadsheetId, string writeRange)
    {
        var appendRequest = _sheetsService.Spreadsheets.Values.Append(valueDataRange, spreadsheetId, writeRange);
        appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.RAW;
        appendRequest.InsertDataOption = SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum.INSERTROWS;
        appendRequest.Execute();
    }

    public int? GetSheetIdByName(string spreadsheetId, string sheetName)
    {
        var s = _sheetsService.Spreadsheets.Get(spreadsheetId);
        var response = s.Execute();
        var sheetId = response.Sheets.FirstOrDefault(s => s.Properties.Title == sheetName);
        return sheetId?.Properties.SheetId;
    }
}
