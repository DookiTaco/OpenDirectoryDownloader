using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace OpenDirectoryDownloader.Site.GDIndex.Bhadoo;

public partial class BhadooIndexResponse
{
	[JsonProperty("nextPageToken")]
	public string NextPageToken { get; set; }

	[JsonProperty("curPageIndex")]
	public long CurPageIndex { get; set; }

	[JsonProperty("data")]
	public Data Data { get; set; }

	[JsonProperty("error")]
	public Error Error { get; set; }
}

public partial class Error
{
	[JsonProperty("code")]
	public int Code { get; set; }

	[JsonProperty("message")]
	public string Message { get; set; }
}

public partial class Data
{
	[JsonProperty("nextPageToken")]
	public string NextPageToken { get; set; }

	[JsonProperty("files")]
	public List<File> Files { get; set; }

	[JsonProperty("error")]
	public DataError Error { get; set; }
}

public partial class DataError
{
	[JsonProperty("errors")]
	public List<ErrorElement> Errors { get; set; }

	[JsonProperty("code")]
	public long Code { get; set; }

	[JsonProperty("message")]
	public string Message { get; set; }
}

public partial class ErrorElement
{
	[JsonProperty("domain")]
	public string Domain { get; set; }

	[JsonProperty("reason")]
	public string Reason { get; set; }

	[JsonProperty("message")]
	public string Message { get; set; }
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

	[JsonProperty("size")]
	public long Size { get; set; }
}

public partial class BhadooIndexResponse
{
	public static BhadooIndexResponse FromJson(string json) => JsonConvert.DeserializeObject<BhadooIndexResponse>(json, Converter.Settings);
}

public static class Serialize
{
	public static string ToJson(this BhadooIndexResponse self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
