using IdGen;

namespace MiguelAlho.BlazorSiteEditor.Models;

    public class Note
    {
        public string Title {get; set;}= string.Empty;
        public DateTime Date {get;set;} = DateTime.Now;
        public string Author {get; set;}= "Miguel Alho";
        public bool Draft {get; set;} = true;
        public string Url {get;set;} = default!;
        public List<string> Tags {get; set;} = new();
        public Video Video { get; set; } = new();

        public string Summary {get; set;} = string.Empty;
        public List<Entry> Notes {get; set;} = new List<Entry>();
        public string Content {get;set;} = string.Empty;

        public string ImageFolderId { get; set; } = new IdGenerator(0).CreateId().ToString();

    }

    public class Entry
    {
        public int Id { get; set; }
        public EntryType Type {get;set;} = EntryType.Quote;
        public string Time {get;set;} = string.Empty;
        public string Image {get;set;} = string.Empty;
        public string Title {get;set;} = string.Empty;
        public string Content {get;set;} = string.Empty;
        public string Comment {get;set;} = string.Empty;
    }

    public enum EntryType
    {
        Quote,
        Slide,
        Note,        
    }

    public class Video
    {
        public string Source {get;set;} = "youtube";
        public string Id {get;set;} = string.Empty;
        public string Link {get;set;} = string.Empty;
        public Author Author { get; set; } = new();
        public Context Context {get;set;} = new Context();
    }

    public class Author
    {
        public string Name {get;set;} = string.Empty;
        public string Twitter {get;set;} = string.Empty;
        public string LinkedIn {get;set;} = string.Empty;
    }

    public class Context
    {
        public string Name {get;set;} = string.Empty;
        public string Link {get;set;} = string.Empty;
    }