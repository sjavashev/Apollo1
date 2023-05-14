using System.Net;
using System.Net.Http.Json;
using MissionShared1;
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
			var op_houston = await ApiClient.GetAsync<HoustonAnswer>("/");

			if (op_houston.Status == OpStatus.OK) {
				Message = $"Houston: OK - {op_houston.Data.Text} ({op_houston.Data.StatusCode})";
			}
			else {
				Message = $"Houston PANIC: {op_houston.Status} [{op_houston.Description}]";
			}
		}
		catch (Exception e) {
			Console.WriteLine(e);
#if DEBUG
			throw;
#endif
		}
	}

	private void Button_Clicked(object sender, EventArgs e) {
		ThisPage_Appearing(sender, e);
	}

	string _message = "...";
}