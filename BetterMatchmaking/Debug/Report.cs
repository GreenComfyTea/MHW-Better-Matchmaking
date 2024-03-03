using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMatchmaking;
internal class Report
{
	public string Key { get; set; } = string.Empty;

	public DateTime Timestamp { get; set; } = DateTime.Now;

	public string Message { get; set; } = string.Empty;

	public Report(string key, string message)
	{
		Key = key;
		Message = message;
	}
}
