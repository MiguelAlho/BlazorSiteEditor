using System.Text.RegularExpressions;

namespace MiguelAlho.BlazorSiteEditor.Services;

public class YoutubeQueryService
{
    public static async Task<YoutubeMetadata?> GetYoutubeMetadata(string videoWatchUrl)
    {
        var url = new System.Uri(videoWatchUrl);
        var queryParams = System.Web.HttpUtility.ParseQueryString(url.Query);
        var id = queryParams["v"];

        using var client = new HttpClient();
        var content = await client.GetStringAsync(url);

        //Thank you GPT
        Regex regex = new Regex(@"""title"":{""simpleText"":""([^""]+)""}");
        Match match = regex.Match(content);
        if (match.Success)
        {
            return new()
            {
                Id = id,
                EmbedLink = $"https://www.youtube.com/embed/{id}",
                Title = match.Groups[1].Value,
            };
        }

        return null;
    }

    public class YoutubeMetadata
    {
        public string Id { get; set; }
        public string EmbedLink { get; set; }
        public string Title { get; set; }
    }
}