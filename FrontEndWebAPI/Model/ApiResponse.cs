namespace FrontEndWebAPI.Model;

public enum ResponseType { GET, ADD, EDIT, DELETE, TOOLKIT }

public class ApiResponse
{
    public bool Success { get { return Errors == null && ValidationErrors == null; } }
    public string? Message { get; set; }
    public object? Data { get; set; }

    public List<string>? Errors { get; set; }
    public IDictionary<string, string[]>? ValidationErrors { get; set; }

    public ApiResponse() { }

    public ApiResponse(Exception exception, string message)
    {
#if RELEASE
        Message = message;

        AddError($"{exception?.Message}");

        if (exception?.InnerException is not null)
        {
            AddError($"{exception?.InnerException?.Message}");
        }
#else
        Message = message;

        AddError($"{exception.GetType()} : {exception?.Message} : StackTrace: {exception?.StackTrace}");

        if (exception?.InnerException is not null)
        {
            AddError($"{exception.InnerException?.GetType()} : {exception?.InnerException?.Message}");
        }
#endif

    }

    public ApiResponse(ResponseType actionType, object data, string title)
    {
        Data = data;

        // Set message text based on data type
        switch (actionType)
        {
            case ResponseType.GET:
                switch (data)
                {
                    case IEnumerable<object> list:
                        Message = $"{list.ToList().Count} {title}(s) found.";
                        break;
                    case null:
                        Message = $"{title}(s) not found.";
                        break;
                    case not null:
                        Message = $"{title}(s) found.";
                        break;
                }
                break;
            case ResponseType.ADD:
                Message = $"{title} added sucessfully. {System.Text.Json.JsonSerializer.Serialize(data)}";
                break;
            case ResponseType.EDIT:
                Message = $"{title} updated sucessfully. [RowsAffected:{data}]";
                break;
            case ResponseType.TOOLKIT:
                Message = $"{title}";
                break;
            default:
                break;
        }

    }

    public void AddError(string message)
    {
        Errors ??= new List<string>();
        Errors.Add(message);
    }

    public string GetErrors()
    {
        string formatedErrorString = "";
        foreach (var errorMsg in Errors)
        {
            formatedErrorString += " :: " + errorMsg;
        }
        return formatedErrorString;
    }


}
