var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => new HoustonAnswer(StatusCode: 1001, Text: "This is Houston!"));

#if DEBUG
app.Urls.Add(MissionShared1.Tools.ApiClient.BASE_URL + "/");
#endif

app.Run();

public record HoustonAnswer(int StatusCode, string Text);
