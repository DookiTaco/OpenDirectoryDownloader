using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace OpenDirectoryDownloader.Site.Pixeldrain.ListResult;

public partial class PixeldrainListResult
{
	[JsonProperty("type")]
	public string Type { get; set; }

	[JsonProperty("api_response")]
	public ApiResponse ApiResponse { get; set; }

	[JsonProperty("captcha_key")]
	public string CaptchaKey { get; set; }

	[JsonProperty("view_token")]
	public string ViewToken { get; set; }

	[JsonProperty("embedded")]
	public bool Embedded { get; set; }

	[JsonProperty("user_ads_enabled")]
	public bool UserAdsEnabled { get; set; }
}

public partial class ApiResponse
{
	[JsonProperty("id")]
	public string Id { get; set; }

	[JsonProperty("title")]
	public string Title { get; set; }

	[JsonProperty("date_created")]
	public DateTimeOffset DateCreated { get; set; }

	[JsonProperty("file_count")]
	public long FileCount { get; set; }

	[JsonProperty("files")]
	public File[] Files { get; set; }

	[JsonProperty("can_edit")]
	public bool CanEdit { get; set; }
}

public partial class File
{
	[JsonProperty("detail_href")]
	public string DetailHref { get; set; }

	[JsonProperty("description")]
	public string Description { get; set; }

	[JsonProperty("id")]
	public string Id { get; set; }

	[JsonProperty("name")]
	public string Name { get; set; }

	[JsonProperty("size")]
	public long Size { get; set; }

	[JsonProperty("views")]
	public long Views { get; set; }

	[JsonProperty("bandwidth_used")]
	public long BandwidthUsed { get; set; }

	[JsonProperty("bandwidth_used_paid")]
	public long BandwidthUsedPaid { get; set; }

	[JsonProperty("downloads")]
	public long Downloads { get; set; }

	[JsonProperty("date_upload")]
	public DateTimeOffset DateUpload { get; set; }

	[JsonProperty("date_last_view")]
	public DateTimeOffset DateLastView { get; set; }

	[JsonProperty("mime_type")]
	public string MimeType { get; set; }

	[JsonProperty("thumbnail_href")]
	public string ThumbnailHref { get; set; }

	[JsonProperty("hash_sha256")]
	public string HashSha256 { get; set; }

	[JsonProperty("availability")]
	public string Availability { get; set; }

	[JsonProperty("availability_message")]
	public string AvailabilityMessage { get; set; }

	[JsonProperty("abuse_type")]
	public string AbuseType { get; set; }

	[JsonProperty("abuse_reporter_name")]
	public string AbuseReporterName { get; set; }

	[JsonProperty("can_edit")]
	public bool CanEdit { get; set; }

	[JsonProperty("show_ads")]
	public bool ShowAds { get; set; }

	[JsonProperty("allow_video_player")]
	public bool AllowVideoPlayer { get; set; }

	[JsonProperty("download_speed_limit")]
	public long DownloadSpeedLimit { get; set; }
}

public partial class PixeldrainListResult
{
	public static PixeldrainListResult FromJson(string json) => JsonConvert.DeserializeObject<PixeldrainListResult>(json, Converter.Settings);
}

public static class Serialize
{
	public static string ToJson(this PixeldrainListResult self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
