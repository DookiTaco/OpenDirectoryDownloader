using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace OpenDirectoryDownloader.Site.Mediafire;

public partial class MediafireResult
{
	[JsonProperty("response")]
	public Response Response { get; set; }
}

public partial class Response
{
	[JsonProperty("action")]
	public string Action { get; set; }

	[JsonProperty("asynchronous")]
	public string Asynchronous { get; set; }

	[JsonProperty("folder_content")]
	public FolderContent FolderContent { get; set; }

	[JsonProperty("result")]
	public string Result { get; set; }

	[JsonProperty("current_api_version")]
	public string CurrentApiVersion { get; set; }
}

public partial class FolderContent
{
	[JsonProperty("chunk_size")]
	public long ChunkSize { get; set; }

	[JsonProperty("content_type")]
	public string ContentType { get; set; }

	[JsonProperty("chunk_number")]
	public long ChunkNumber { get; set; }

	[JsonProperty("folderkey")]
	public string Folderkey { get; set; }

	[JsonProperty("folders")]
	public Folder[] Folders { get; set; }

	[JsonProperty("files")]
	public File[] Files { get; set; }

	[JsonProperty("more_chunks")]
	public string MoreChunks { get; set; }

	[JsonProperty("revision")]
	public long Revision { get; set; }
}

public partial class File
{
	[JsonProperty("quickkey")]
	public string Quickkey { get; set; }

	[JsonProperty("hash")]
	public string Hash { get; set; }

	[JsonProperty("filename")]
	public string Filename { get; set; }

	[JsonProperty("description")]
	public string Description { get; set; }

	[JsonProperty("size")]
	public long Size { get; set; }

	[JsonProperty("privacy")]
	public string Privacy { get; set; }

	[JsonProperty("created")]
	public DateTimeOffset Created { get; set; }

	[JsonProperty("password_protected")]
	public string PasswordProtected { get; set; }

	[JsonProperty("mimetype")]
	public string Mimetype { get; set; }

	[JsonProperty("filetype")]
	public string Filetype { get; set; }

	[JsonProperty("view")]
	public long View { get; set; }

	[JsonProperty("edit")]
	public long Edit { get; set; }

	[JsonProperty("revision")]
	public long Revision { get; set; }

	[JsonProperty("flag")]
	public long Flag { get; set; }

	[JsonProperty("permissions")]
	public Permissions Permissions { get; set; }

	[JsonProperty("downloads")]
	public long Downloads { get; set; }

	[JsonProperty("views")]
	public long Views { get; set; }

	[JsonProperty("links")]
	public Links Links { get; set; }

	[JsonProperty("created_utc")]
	public DateTimeOffset CreatedUtc { get; set; }
}

public partial class Links
{
	[JsonProperty("normal_download")]
	public Uri NormalDownload { get; set; }
}

public partial class Permissions
{
	[JsonProperty("value")]
	public long Value { get; set; }

	[JsonProperty("explicit")]
	public long Explicit { get; set; }

	[JsonProperty("read")]
	public long Read { get; set; }

	[JsonProperty("write")]
	public long Write { get; set; }
}

public partial class Folder
{
	[JsonProperty("folderkey")]
	public string Folderkey { get; set; }

	[JsonProperty("name")]
	public string Name { get; set; }

	[JsonProperty("description")]
	public string Description { get; set; }

	[JsonProperty("tags")]
	public string Tags { get; set; }

	[JsonProperty("privacy")]
	public string Privacy { get; set; }

	[JsonProperty("created")]
	public DateTimeOffset Created { get; set; }

	[JsonProperty("revision")]
	public long Revision { get; set; }

	[JsonProperty("flag")]
	public long Flag { get; set; }

	[JsonProperty("permissions")]
	public Permissions Permissions { get; set; }

	[JsonProperty("file_count")]
	public long FileCount { get; set; }

	[JsonProperty("folder_count")]
	public long FolderCount { get; set; }

	[JsonProperty("dropbox_enabled")]
	public string DropboxEnabled { get; set; }

	[JsonProperty("created_utc")]
	public DateTimeOffset CreatedUtc { get; set; }
}

public partial class MediafireResult
{
	public static MediafireResult FromJson(string json) => JsonConvert.DeserializeObject<MediafireResult>(json, Converter.Settings);
}

public static class Serialize
{
	public static string ToJson(this MediafireResult self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
