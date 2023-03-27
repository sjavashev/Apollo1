using System;
using System.Net;

namespace MissionShared1.Tools {
	public partial class ApiClient {
		public static async Task<HttpResponseMessage> GetAsync(string sub_url) {
			var url = String.Join("/", new[] { BASE_URL, sub_url?.Trim('/') });
			var res = await _http_client.GetAsync(url);
			return res;
		}

		private ApiClient() { }

		static ApiClient() {
#pragma warning disable CS0162
			if (IS_MOCK_SSL) {
				ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };
				_http_client = new HttpClient(new HttpClientHandler() {
					ServerCertificateCustomValidationCallback = delegate { return true; },
				});
			}
			else {
				_http_client = new HttpClient();
			}
#pragma warning restore CS0162
		}

		static HttpClient _http_client;

#if !DEBUG
		public const string BASE_URL = "https://my.real.backendurl.com:16384";
		const bool IS_MOCK_SSL = false;
#endif
	}
}

