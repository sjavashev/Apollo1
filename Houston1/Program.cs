using Houston1;
using MissionShared1;
using MissionShared1.Tools;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => GetHoustonAnswer().GetApiResult());

#if DEBUG
app.Urls.Add(MissionShared1.Tools.ApiClient.BASE_URL + "/");
#endif

app.Run();


OpResult<HoustonAnswer?> GetHoustonAnswer() {
	var random = new Random().Next(10);

	switch (random) {
		case 1:
			return OpResult<HoustonAnswer?>.Fail(OpStatus.Forbidden);
		case 3:
			return OpResult<HoustonAnswer?>.Fail(OpStatus.AlreadyExists, $"Already exists because {random}");
		case 5:
			return OpResult<HoustonAnswer?>.Fail(OpStatus.NotFound, $"Not found as {random}");
		case 7:
			return OpResult<HoustonAnswer?>.Fail(description: $"Just failed since {random}");
	}

	var res = new HoustonAnswer(StatusCode: 1001, Text: "This is Houston!");
	return new(res);
}
