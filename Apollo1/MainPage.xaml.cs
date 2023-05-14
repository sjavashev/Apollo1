using System.Net;
using System.Net.Http.Json;
using MissionShared1.Tools;

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
			var response = await ApiClient.GetAsync("/");
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