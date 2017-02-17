#r "Newtonsoft.Json"

using System;
using System.Net;
using Newtonsoft.Json;

public static async Task<object> Run(HttpRequestMessage req, TraceWriter log)
{
    var badTags = new List<string>() { "outdoor" };
    bool goodImage = true;
    

    string jsonContent = await req.Content.ReadAsStringAsync();
    dynamic data = JsonConvert.DeserializeObject(jsonContent);

    foreach(var category in data["tags"])
    {
        if(badTags.Contains((string)category["name"])) {
            goodImage = false;
        }
    }

    return req.CreateResponse(HttpStatusCode.OK, goodImage.ToString());
}
