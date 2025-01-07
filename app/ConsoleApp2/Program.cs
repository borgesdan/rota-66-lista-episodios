using ConsoleApp2;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

var rotaApi = new RotaApi();
using var client = new HttpClient();
var response = new ClipResponse();

while (true)
{
    var url = rotaApi.ToString();
    var result = await client.GetAsync(url);
    var clip = await result.Content.ReadFromJsonAsync<ClipResponse>();

    if (clip == null || !clip.Clips.Any())
        break;

    var last = clip.Clips.Last();
    var responseLast = response.Clips.LastOrDefault();

    if (responseLast != null && responseLast.Id == last.Id)
        break;

    response.Clips.AddRange(clip.Clips);
    rotaApi.ClipId = last.Id.ToString();

    await Task.Delay(2000);
}

var json = JsonSerializer.Serialize(response.Clips);
File.AppendAllText("c:/rota66.json", json);

var clipsOrdened = response.Clips.DistinctBy(c => c.Id).OrderBy(c => c.Episode).ToList();

StringBuilder sb = new StringBuilder();

foreach (var cl in clipsOrdened)
{
    sb.AppendLine($"- {cl.Episode} - {cl.Title}");
}

File.AppendAllText("c:/rota66Ordened.txt", sb.ToString());
