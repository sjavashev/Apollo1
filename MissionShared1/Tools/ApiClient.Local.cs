namespace MissionShared1.Tools {
	public partial class ApiClient {
#if DEBUG
		public const string BASE_URL = "https://192.168.1.64:16000";
		const bool IS_MOCK_SSL = true;
#endif
	}
}