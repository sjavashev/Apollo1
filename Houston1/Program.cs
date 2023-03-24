var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => new HoustonAnswer(StatusCode: 1001, Text: "This is Houston!"));
app.Urls.Add("https://192.168.1.64:16000/");
app.Run();

public record HoustonAnswer(int StatusCode, string Text);
