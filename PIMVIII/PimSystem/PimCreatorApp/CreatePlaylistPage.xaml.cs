using System.Net.Http.Json;

namespace PimCreatorApp;

public partial class CreatePlaylistPage : ContentPage
{
	public CreatePlaylistPage()
	{
		InitializeComponent();
	}

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text)) return;

        LoadingSpinner.IsRunning = true;

        var novaPlaylist = new 
        { 
            Title = NameEntry.Text, 
            Description = DescEntry.Text,
            UserID = App.CurrentUserId 
        };

        try 
        {
            string apiUrl = "http://localhost:5207/api/Playlists"; 
            using (HttpClient client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync(apiUrl, novaPlaylist);
                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlertAsync("Sucesso", "Playlist criada com sucesso!", "OK");
                    await Navigation.PopAsync(); 
                }
                else 
                {
                    await DisplayAlertAsync("Erro", "Não deu pra salvar. Código: " + response.StatusCode, "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", ex.Message, "OK");
        }
        finally
        {
            LoadingSpinner.IsRunning = false;
        }
    }
}