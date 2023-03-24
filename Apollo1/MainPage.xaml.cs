using System.Net;
using System.Net.Http.Json;

namespace Apollo1;

public partial class MainPage : ContentPage {
	public MainPage() {
		InitializeComponent();
	}

	public string Message {
		get => _message;
		set {
			_message = value;
			OnPropertyChanged(nameof(Message));
		}
	}

	async void ThisPage_Appearing(System.Object sender, System.EventArgs evt) {
		try {
			ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };
			using var client = new HttpClient(new HttpClientHandler() {
				ServerCertificateCustomValidationCallback = delegate { return true; },
			});

			var response = await client.GetAsync("https://192.168.1.64:16000");
			var json = await response.Content.ReadAsStringAsync();
			Message = json;
		}
		catch (Exception e) {
			Console.WriteLine(e);
#if DEBUG
			throw;
#endif
		}
	}

	string _message = "...";
}