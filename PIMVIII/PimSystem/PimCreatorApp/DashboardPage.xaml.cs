using System.Net.Http.Json; 

namespace PimCreatorApp;

public partial class DashboardPage : ContentPage
{
    private const string ApiUrl = "http://localhost:5207/api/Playlists"; 

	public DashboardPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarPlaylists();
    }

    private async Task CarregarPlaylists()
    {
        StatusLabel.IsVisible = true;
        StatusLabel.Text = "Buscando gado no pasto...";

        try
        {
            using (HttpClient client = new HttpClient())
            {
                var todasPlaylists = await client.GetFromJsonAsync<List<Playlist>>(ApiUrl);

                var minhasPlaylists = todasPlaylists.Where(p => p.userId == App.CurrentUserId).ToList();

                PlaylistsStack.Children.Clear();

                var btnCriar = new Button 
                { 
                    Text = "+ Nova Playlist", 
                    BackgroundColor = Color.FromArgb("#512BD4"), 
                    TextColor = Colors.White,
                    HeightRequest = 50,
                    CornerRadius = 10,
                    FontAttributes = FontAttributes.Bold
                };
                btnCriar.Clicked += OnNewPlaylistClicked; 
                PlaylistsStack.Children.Add(btnCriar);

                foreach (var playlist in minhasPlaylists)
                {
                    var frame = new Frame
                    {
                        BackgroundColor = Color.FromArgb("#333333"),
                        Padding = 15,
                        CornerRadius = 10,
                        BorderColor = Colors.Transparent,
                        Margin = new Thickness(0, 0, 0, 10) 
                    };

                    var stack = new VerticalStackLayout();
                    
                    stack.Children.Add(new Label { Text = playlist.title, TextColor = Colors.White, FontSize = 18, FontAttributes = FontAttributes.Bold });
                    stack.Children.Add(new Label { Text = playlist.description, TextColor = Color.FromArgb("#CCCCCC"), FontSize = 14 });

                    var btnAdd = new Button
                    {
                        Text = "🎵 Adicionar Música",
                        FontSize = 12,
                        HeightRequest = 40,
                        BackgroundColor = Color.FromArgb("#444444"),
                        TextColor = Colors.White,
                        Margin = new Thickness(0, 10, 0, 0),
                        CornerRadius = 5
                    };

                    btnAdd.Clicked += async (s, e) => 
                    {
                        await Navigation.PushAsync(new AddContentPage(playlist.id, playlist.title));
                    };

                    stack.Children.Add(btnAdd);

                    frame.Content = stack;
                    PlaylistsStack.Children.Add(frame);
                }

                if (minhasPlaylists.Count == 0)
                {
                    PlaylistsStack.Children.Add(new Label { Text = "Nenhuma playlist encontrada. Crie a primeira, cowboy!", TextColor = Colors.Gray, HorizontalOptions = LayoutOptions.Center });
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro", "Falha ao buscar playlists: " + ex.Message, "OK");
        }
        finally
        {
            StatusLabel.IsVisible = false;
        }
    }

    private async void OnNewPlaylistClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreatePlaylistPage());
    }
}