using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace MissionShared1.Tools {
	public partial class ApiClient {
		public static async Task<OpResult<T>> GetAsync<T>(string sub_url) where T : class {
			var url = String.Join("/", new[] { BASE_URL, sub_url?.Trim('/') });
			var msg = await _http_client.GetAsync(url);
			var res = await get_response_result<T>(msg);
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


		private static async Task<OpResult<TResult>> get_response_result<TResult>(HttpResponseMessage response) where TResult : class {
			OpResult<TResult> res;

			if (response.StatusCode == HttpStatusCode.Forbidden) {
				res = OpResult<TResult>.Fail(OpStatus.Forbidden);
			}
			else if (response.StatusCode == HttpStatusCode.Conflict) {
				var description = await response.Content.ReadAsStringAsync();
				res = OpResult<TResult>.Fail(OpStatus.AlreadyExists, description);
			}
			else if (response.StatusCode == HttpStatusCode.NotFound) {
				var description = await response.Content.ReadAsStringAsync();
				res = OpResult<TResult>.Fail(OpStatus.NotFound, description);
			}
			else if (response.StatusCode != HttpStatusCode.OK) {
				var description = await response.Content.ReadAsStringAsync();
				res = new OpResult<TResult>(null, OpStatus.Fail, description);
			}
			else {
				using var stream = await response.Content.ReadAsStreamAsync();
				var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
				var data = await JsonSerializer.DeserializeAsync<TResult>(stream, options);
				res = new OpResult<TResult>(data);
			}

			return res;
		}

		static HttpClient _http_client;

#if !DEBUG
		public const string BASE_URL = "https://my.real.backendurl.com:16384";
		const bool IS_MOCK_SSL = false;
#endif
	}
}

