using ABI.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Sockets;

namespace BetterMatchmaking;

internal class DebugManager
{
	// Singleton Pattern
	private static readonly DebugManager _singleton = new();

	public static DebugManager Instance => _singleton;

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static DebugManager() { }

	// Singleton Pattern End

	public Dictionary<string, Report> Reports = new();
	public Queue<Report> History = new();

	public DebugCustomization Customization { get; set; }

	private DebugManager() { }

	public void Report(string key, string message)
	{
		message = message.Replace(Constants.REPO_PATH, "").Replace("BetterMatchmaking.", "");

		var newReport = new Report(key, message);

		Reports[key] = newReport;

		TeaLog.Error(message);

		AddToHistory(newReport);
	}

	private void AddToHistory(Report report)
	{
		if(Customization.HistorySize == 0) return;

		if(History.Count >= Customization.HistorySize)
		{
			History.TryDequeue(out _);
		}

		History.Enqueue(report);
	}

	public void TrimHistory()
	{
		while (History.Count > Customization.HistorySize)
		{ 
			History.Dequeue();
		}
	}
}
