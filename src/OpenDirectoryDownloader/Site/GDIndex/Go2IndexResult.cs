using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace OpenDirectoryDownloader.Site.GDIndex.Go2Index;

public partial class Go2IndexResponse
{
	[JsonProperty("nextPageToken")]
	public string NextPageToken { get; set; }

	[JsonProperty("curPageIndex")]
	public int CurPageIndex { get; set; }

	[JsonProperty("data")]
	public Data Data { get; set; }

	[JsonProperty("error")]
	public Error Error { get; set; }
}

public partial class Data
{
	[JsonProperty("files")]
	public List<File> Files { get; set; }

	// Needed for alx-xlx/goindex
	[JsonProperty("error")]
	public Error Error { get; set; }
}

public partial class File
{
	[JsonProperty("id")]
	public string Id { get; set; }

	[JsonProperty("name")]
	public string Name { get; set; }

	[JsonProperty("mimeType")]
	public string MimeType { get; set; }

	[JsonProperty("modifiedTime")]
	public DateTimeOffset ModifiedTime { get; set; }

	[JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
	public long Size { get; set; }
}

public partial class Error
{
	[JsonProperty("code")]
	public int Code { get; set; }

	[JsonProperty("message")]
	public string Message { get; set; }
}

public partial class Go2IndexResponse
{
	public static Go2IndexResponse FromJson(string json) => JsonConvert.DeserializeObject<Go2IndexResponse>(json, Converter.Settings);
}

public static class Serialize
{
	public static string ToJson(this Go2IndexResponse self) => JsonConvert.SerializeObject(self, Converter.Settings);
}

internal static class Converter
{
	public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
	{
		MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
		DateParseHandling = DateParseHandling.None,
		Converters =
		{
			new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
		},
	};
}
