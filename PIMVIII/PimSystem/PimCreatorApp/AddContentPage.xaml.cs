using System.Net.Http.Json;

namespace PimCreatorApp;

public partial class AddContentPage : ContentPage
{
    private int _playlistId;
    private const string ApiUrl = "http://localhost:5207/api/Contents/addToPlaylist"; 
    public AddContentPage(int playlistId, string playlistName)
    {
        InitializeComponent();
        _playlistId = playlistId;
        PlaylistLabel.Text = $"Adicionando em: {playlistName}";
    }

    private async void OnUploadClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TitleEntry.Text)) 
        {
            await DisplayAlert("Erro", "O boi precisa de nome!", "OK");
            return;
        }

        LoadingSpinner.IsRunning = true;

        var novoConteudo = new 
        { 
            Title = TitleEntry.Text, 
            Type = TypePicker.SelectedItem.ToString(),
            Url = UrlEntry.Text,
            PlaylistID = _playlistId
        };

        try 
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync(ApiUrl, novoConteudo);
                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Sucesso", "Conteúdo adicionado na playlist!", "Show");
                    await Navigation.PopAsync(); 
                }
                else 
                {
                    await DisplayAlert("Erro", "Falha no upload. Código: " + response.StatusCode, "Vixe");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro Técnico", ex.Message, "OK");
        }
        finally
        {
            LoadingSpinner.IsRunning = false;
        }
    }
}