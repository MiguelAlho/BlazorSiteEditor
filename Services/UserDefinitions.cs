using System.Text.Json;

namespace MiguelAlho.BlazorSiteEditor.Services;

class UserDefinitionsService
{
    private static string FilePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/blazorsiteeditoruserdefinitions.json";

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

    public static bool Exists() => File.Exists(FilePath);


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
        

}

public class UserDefinitions
{
    public string SiteRepositoryPath {get;set;} = string.Empty;
    public string BookmarksFolderPath {get;set;} = "/content/bookmarks";
    public string PostsFolderPath {get;set;} = "/content/posts";
    public string BookmarkImagesBaseFolderPath { get; set; } = "/static/bookmarks";
}