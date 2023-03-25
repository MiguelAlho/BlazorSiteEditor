using System.Text.Json;

namespace MiguelAlho.BlazorSiteEditor.Services;

/// <summary>
/// Carries machine / User specific settings. I use two different machines (Windows and Mac) so paths can be different on each,
/// but I want to be able to use and edit, when possible the editor on those.   
/// </summary>
class UserDefinitionsService
{
    private static string FilePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/blazorsiteeditoruserdefinitions.json";

    public static bool Exists() => File.Exists(FilePath);
    
    public static void Save(UserDefinitions definitions)
    {
        Console.WriteLine(FilePath);

        var json = JsonSerializer.Serialize(definitions);
        File.WriteAllText(FilePath, json);
    }

    public static UserDefinitions Load()
    {
        if(!Exists())
            return new UserDefinitions();
        
        return JsonSerializer.Deserialize<UserDefinitions>(File.ReadAllText(FilePath)) ?? new();
    }

    
    public static string SiteRepositoryPath => Load().SiteRepositoryPath;
    
    public static string BookmarksFolderPath {
        get{
            var definitions = Load();
            return $"{definitions.SiteRepositoryPath}{definitions.BookmarksFolderPath}";
        }
    }

    public static string BookmarkImagesBaseFolderPath
    {
        get
        {
            var definitions = Load();
            return $"{definitions.SiteRepositoryPath}{definitions.BookmarkImagesBaseFolderPath}";
        }
    }
    
    public static string LocalHugoUrl => Load().LocalHugoUrl;
}

public class UserDefinitions
{
    public string SiteRepositoryPath {get;set;} = string.Empty;
    public string BookmarksFolderPath {get;set;} = "/content/bookmarks";
    public string PostsFolderPath {get;set;} = "/content/posts";
    public string BookmarkImagesBaseFolderPath { get; set; } = "/static/bookmarks";
    
    public string LocalHugoUrl {get;set;} = "http://localhost:1313";
}