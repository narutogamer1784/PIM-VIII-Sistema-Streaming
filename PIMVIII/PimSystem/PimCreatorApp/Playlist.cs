namespace PimCreatorApp;

public class Playlist
{
    public int id { get; set; }
    
    public string title { get; set; } = string.Empty; 
    
    public string? description { get; set; } 
    
    public int userId { get; set; }
}