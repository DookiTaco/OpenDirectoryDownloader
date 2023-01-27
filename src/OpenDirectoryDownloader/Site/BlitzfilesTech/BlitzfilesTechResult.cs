using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace OpenDirectoryDownloader.Site.BlitzfilesTech;

public partial class BlitzfilesTechResponse
{
	[JsonProperty("link")]
	public Link Link { get; set; }

	[JsonProperty("folderChildren")]
	public FolderChildren FolderChildren { get; set; }

	[JsonProperty("status")]
	public string Status { get; set; }

	[JsonProperty("seo")]
	public object Seo { get; set; }
}

public partial class FolderChildren
{
	[JsonProperty("current_page")]
	public long CurrentPage { get; set; }

	[JsonProperty("data")]
	public List<Entry> Data { get; set; }

	[JsonProperty("from")]
	public long From { get; set; }

	[JsonProperty("last_page")]
	public long LastPage { get; set; }

	[JsonProperty("next_page_url")]
	public string NextPageUrl { get; set; }

	[JsonProperty("path")]
	public string Path { get; set; }

	[JsonProperty("per_page")]
	public long PerPage { get; set; }

	[JsonProperty("prev_page_url")]
	public string PrevPageUrl { get; set; }

	[JsonProperty("to")]
	public long To { get; set; }

	[JsonProperty("total")]
	public long Total { get; set; }
}

public partial class Entry
{
	[JsonProperty("id")]
	public long Id { get; set; }

	[JsonProperty("name")]
	public string Name { get; set; }

	[JsonProperty("description")]
	public object Description { get; set; }

	[JsonProperty("file_name")]
	public string FileName { get; set; }

	[JsonProperty("mime")]
	public string Mime { get; set; }

	[JsonProperty("file_size")]
	public long FileSize { get; set; }

	[JsonProperty("user_id")]
	public object UserId { get; set; }

	[JsonProperty("parent_id")]
	public long? ParentId { get; set; }

	[JsonProperty("password")]
	public object Password { get; set; }

	[JsonProperty("created_at")]
	public DateTimeOffset CreatedAt { get; set; }

	[JsonProperty("updated_at")]
	public DateTimeOffset UpdatedAt { get; set; }

	[JsonProperty("deleted_at")]
	public object DeletedAt { get; set; }

	[JsonProperty("path")]
	public string Path { get; set; }

	[JsonProperty("disk_prefix")]
	public string DiskPrefix { get; set; }

	[JsonProperty("type")]
	public string Type { get; set; }

	[JsonProperty("extension")]
	public string Extension { get; set; }

	[JsonProperty("public")]
	public bool Public { get; set; }

	[JsonProperty("thumbnail")]
	public bool Thumbnail { get; set; }

	[JsonProperty("hash")]
	public string Hash { get; set; }

	[JsonProperty("url")]
	public string Url { get; set; }

	[JsonProperty("users")]
	public List<User> Users { get; set; }
}

public partial class User
{
	[JsonProperty("email")]
	public string Email { get; set; }

	[JsonProperty("id")]
	public long Id { get; set; }

	[JsonProperty("avatar")]
	public Uri Avatar { get; set; }

	[JsonProperty("owns_entry")]
	public long OwnsEntry { get; set; }

	[JsonProperty("entry_permissions")]
	public object EntryPermissions { get; set; }

	[JsonProperty("display_name")]
	public string DisplayName { get; set; }
}

public partial class Link
{
	[JsonProperty("id")]
	public long Id { get; set; }

	[JsonProperty("hash")]
	public string Hash { get; set; }

	[JsonProperty("user_id")]
	public long UserId { get; set; }

	[JsonProperty("entry_id")]
	public long EntryId { get; set; }

	[JsonProperty("allow_edit")]
	public bool AllowEdit { get; set; }

	[JsonProperty("allow_download")]
	public bool AllowDownload { get; set; }

	[JsonProperty("password")]
	public object Password { get; set; }

	[JsonProperty("expires_at")]
	public object ExpiresAt { get; set; }

	[JsonProperty("created_at")]
	public DateTimeOffset CreatedAt { get; set; }

	[JsonProperty("updated_at")]
	public DateTimeOffset UpdatedAt { get; set; }

	[JsonProperty("entry")]
	public Entry Entry { get; set; }
}

public partial class BlitzfilesTechResponse
{
	public static BlitzfilesTechResponse FromJson(string json) => JsonConvert.DeserializeObject<BlitzfilesTechResponse>(json, Converter.Settings);
}

public static class Serialize
{
	public static string ToJson(this BlitzfilesTechResponse self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
