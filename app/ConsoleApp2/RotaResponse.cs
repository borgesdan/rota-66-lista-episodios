using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace ConsoleApp2
{
    internal class RotaApi
    {
        public string Url { get; set; } = "https://api.omny.fm/programs/rota-66/playlists/podcast/clips";
        public string PageSize { get; set; } = "50";
        public string IncludeProgramDetail { get; set; } = "false";
        public string ClipId { get; set; } = string.Empty;

        public override string ToString()
        {
            var param = new Dictionary<string, string>() 
            { 
                { "pageSize", PageSize },
                { "icludeProgramDetail", IncludeProgramDetail },
                { "clipId", ClipId },
            };

            var newUrl = new Uri(QueryHelpers.AddQueryString(Url, param));
            return newUrl.ToString();
        }
    }

    internal class RotaResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Season { get; set; }
        public int Episode { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Id: ");
            sb.AppendLine(Id.ToString());
            sb.Append("Title: ");
            sb.AppendLine(Title);
            sb.Append("Season: ");
            sb.AppendLine(Season.ToString());
            sb.Append("Episode: ");
            sb.AppendLine(Episode.ToString());

            return sb.ToString();
        }
    }

    internal class ClipResponse
    {
        public List<RotaResponse> Clips { get; set; } = [];
    }
}
