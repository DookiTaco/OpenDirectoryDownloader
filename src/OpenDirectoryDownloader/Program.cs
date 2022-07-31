using CommandLine;
using NLog;
using NLog.Config;
using OpenDirectoryDownloader.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace OpenDirectoryDownloader;

class Program
{
	private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
	public static string ConsoleTitle { get; set; }
	private static CommandLineOptions CommandLineOptions { get; set; }

	static async Task<int> Main(string[] args)
	{
		SetConsoleTitle("OpenDirectoryDownloader");

		Console.OutputEncoding = Encoding.UTF8;

		Stream nlogConfigFile = Library.GetEmbeddedResourceStream(Assembly.GetEntryAssembly(), "NLog.config");

		if (nlogConfigFile != null)
		{
			XmlReader xmlReader = XmlReader.Create(nlogConfigFile);
			LogManager.Configuration = new XmlLoggingConfiguration(xmlReader, null);
		}

		Process currentProcess = Process.GetCurrentProcess();

		Console.WriteLine($"Started with PID {currentProcess.Id}");
		Logger.Info($"Started with PID {currentProcess.Id}");

		Thread.CurrentThread.Name = "Main thread";

		bool stopProcessing = false;

		Parser parser = new Parser(with =>
		{
			with.AllowMultiInstance = true;
			with.HelpWriter = Console.Error;
		});

		parser.ParseArguments<CommandLineOptions>(args)
			.WithNotParsed(o =>
			{
				List<Error> errors = o.ToList();

				stopProcessing = errors.Any(e => e.StopsProcessing || e.Tag == ErrorType.UnknownOptionError);

				if (errors.Any(e => e.Tag == ErrorType.HelpRequestedError || e.Tag == ErrorType.VersionRequestedError || e.Tag == ErrorType.UnknownOptionError))
				{
					return;
				}

				foreach (Error error in errors)
				{
					Console.WriteLine($"Error command line parameter '{error.Tag}'");
				}
			})
			.WithParsed(o => CommandLineOptions = o);

		if (stopProcessing)
		{
			return 1;
		}

		if (CommandLineOptions.Threads < 1 || CommandLineOptions.Threads > 100)
		{
			Console.WriteLine("Threads must be between 1 and 100");
			return 1;
		}

		if (CommandLineOptions.Threads > 1 && CommandLineOptions.WaitSecondsBetweenCalls > 0)
		{
			Logger.Warn("Using a wait time with more than 1 thread isn't recommended as it will still have multiple threads running.");
			return 1;
		}

		string url = CommandLineOptions.Url;

		if (string.IsNullOrWhiteSpace(url))
		{
			Console.WriteLine("Which URL do you want to index?");
			url = Console.ReadLine();
		}

		// Wait until this ticket is closed: https://github.com/dotnet/corefx/pull/37050
		//AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);

		OpenDirectoryIndexerSettings openDirectoryIndexerSettings = new OpenDirectoryIndexerSettings
		{
			CommandLineOptions = CommandLineOptions
		};

		if (File.Exists(url))
		{
			openDirectoryIndexerSettings.FileName = url;
		}
		else
		{
			Console.WriteLine($"URL specified: {url}");

			string newUrl = Library.FixUrl(url);

			if (newUrl != url)
			{
				Console.WriteLine($"URL fixed    : {newUrl}");
			}

			openDirectoryIndexerSettings.Url = newUrl;
		}

		openDirectoryIndexerSettings.Threads = openDirectoryIndexerSettings.CommandLineOptions.Threads;
		openDirectoryIndexerSettings.Timeout = openDirectoryIndexerSettings.CommandLineOptions.Timeout;
		openDirectoryIndexerSettings.Username = openDirectoryIndexerSettings.CommandLineOptions.Username;
		openDirectoryIndexerSettings.Password = openDirectoryIndexerSettings.CommandLineOptions.Password;

		if (string.IsNullOrEmpty(openDirectoryIndexerSettings.Username) && string.IsNullOrEmpty(openDirectoryIndexerSettings.Password))
		{
			if (Library.GetUriCredentials(new Uri(openDirectoryIndexerSettings.Url), out string username, out string password))
			{
				Console.WriteLine($"Using username '{username}' and password '{password}'");
				openDirectoryIndexerSettings.Username = username;
				openDirectoryIndexerSettings.Password = password;
			}
		}

		// FTP
		if (openDirectoryIndexerSettings.Url?.StartsWith(Constants.UriScheme.Ftp) == true || openDirectoryIndexerSettings.Url?.StartsWith(Constants.UriScheme.Ftps) == true)
		{
			openDirectoryIndexerSettings.Threads = 6;
		}

		// Translates . and .. etc
		if (openDirectoryIndexerSettings.CommandLineOptions.OutputFile is not null)
		{
			openDirectoryIndexerSettings.CommandLineOptions.OutputFile = Path.GetFullPath(openDirectoryIndexerSettings.CommandLineOptions.OutputFile);
		}

		OpenDirectoryIndexer openDirectoryIndexer = new OpenDirectoryIndexer(openDirectoryIndexerSettings);

		SetConsoleTitle($"{new Uri(openDirectoryIndexerSettings.Url).Host.Replace("www.", string.Empty)} - {ConsoleTitle}");

		openDirectoryIndexer.StartIndexingAsync();
		Console.WriteLine("Started indexing!");

		Command.ShowInfoAndCommands();
		Command.ProcessConsoleInput(openDirectoryIndexer);

		await openDirectoryIndexer.IndexingTask;

		if (!CommandLineOptions.Quit)
		{
			Console.WriteLine("Press ESC to exit");
			Console.ReadKey();
		}

		return 0;
	}

	public static void SetConsoleTitle(string title)
	{
		ConsoleTitle = title;

		Console.Title = title;
	}
}
