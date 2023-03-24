using System.Text;
using MiguelAlho.BlazorSiteEditor.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace MiguelAlho.BlazorSiteEditor.Services;

class BookmarksService
{
    private static string BookmarksDirectory = UserDefinitionsService.BookmarksFolderPath;

    public static BookmarkFile LoadBookmark(string filename)
    {
        var bookmarkFilePath = GetBookmarkFilePath(filename);
        var bookmarkFileContent = File.ReadAllText(bookmarkFilePath);

        //process and load yaml to data struct
        var parts = bookmarkFileContent.Split("---", StringSplitOptions.RemoveEmptyEntries);
        
        var frontMatter = parts[0];
        try
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance) // see height_in_inches in sample yml 
                .IgnoreUnmatchedProperties()
                .IgnoreFields()
                .Build();
            var bookmarkNote = deserializer.Deserialize<Note>(frontMatter);

            //reset note ids in order
            for (var i = 0; i < bookmarkNote.Notes.Count; i++)
            {
                bookmarkNote.Notes[i].Id = i;
            }

            return new BookmarkFile(bookmarkFilePath)
            {
                Note = bookmarkNote
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine(ex.InnerException);
            throw;
        }
    }
    
    public static bool SaveBookmark(BookmarkFile bookmark)
    {
        var fileContent = new StringBuilder();
        
        try{
            var deserializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)  // see height_in_inches in sample yml 
                .Build();
            var yaml = deserializer.Serialize(bookmark.Note);
            
            fileContent.AppendLine("---");
            fileContent.AppendLine(yaml);
            fileContent.AppendLine("---");
            
            File.WriteAllText(bookmark.FilePath, fileContent.ToString());
            return true;
            
        }catch(Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine(ex.InnerException);
            return false;
        }
    }

    private static string GetBookmarkFilePath(string filename) 
        => Path.Combine(new DirectoryInfo(BookmarksDirectory).FullName, filename);

    public static bool BookmarkExists(string filename) 
        => File.Exists(GetBookmarkFilePath(filename));
}

public class BookmarkFile
{
    public BookmarkFile(string filePath)
    {
        FilePath = filePath;
    }

    public string FilePath { get; }
    public Note Note { get; set; } = new();
}