using System.Net.Http.Json;
using System.Text.Json;

namespace PimCreatorApp;

public partial class MainPage : ContentPage
{
    private const string ApiUrl = "http://localhost:5207/api/Users/login";

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EmailEntry.Text))
        {
            await DisplayAlert("Epa!", "Preciso do e-mail pra te deixar entrar.", "OK");
            return;
        }

        LoginBtn.IsEnabled = false;
        LoadingSpinner.IsRunning = true;

        try
        {
            var loginData = new
            {
                Email = EmailEntry.Text,
                Password = PasswordEntry.Text
            };

            using (HttpClient client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync(ApiUrl, loginData);

                if (response.IsSuccessStatusCode)
                {
                    
                    var userJson = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                    var userObj = JsonSerializer.Deserialize<UserDto>(userJson, options);

                    if (userObj != null)
                    {
                        App.CurrentUserId = userObj.id;
                        App.CurrentUserName = userObj.name;
                    }

                    Application.Current.MainPage = new NavigationPage(new DashboardPage());
                    
                }
                else
                {
                    await DisplayAlert("Acesso Negado", "Esse e-mail não consta na lista do Xerife.", "Tentar de novo");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro de Conexão", $"Não achei a API.\nEla está rodando?\nErro: {ex.Message}", "Vou conferir");
        }
        finally
        {
            LoginBtn.IsEnabled = true;
            LoadingSpinner.IsRunning = false;
        }
    }
}

public class UserDto 
{ 
    public int id { get; set; } 
    public string name { get; set; } 
}