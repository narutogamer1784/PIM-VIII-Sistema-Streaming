namespace PimCreatorApp;

public partial class App : Application
{
    public static int CurrentUserId { get; set; } 
    public static string CurrentUserName { get; set; }

    public App()
    {
        InitializeComponent();
        MainPage = new AppShell(); 
    }
}