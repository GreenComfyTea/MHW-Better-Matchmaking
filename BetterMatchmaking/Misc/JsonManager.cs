using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace BetterMatchmaking;

public static class JsonManager
{
	public static readonly JsonSerializerOptions JSON_SERIALIZER_OPTIONS_INSTANCE = new()
	{
		WriteIndented = true,
		AllowTrailingCommas = true,
		Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
	};

	public static string Serialize(object obj)
	{
		return JsonSerializer.Serialize(obj, JSON_SERIALIZER_OPTIONS_INSTANCE).Replace("  ", "\t");
	}

	public static void SerializeToFile(string filePathName, string json)
	{
		//File.WriteAllText(filePathName, json);
		Directory.CreateDirectory(Path.GetDirectoryName(filePathName));
		var file = File.Open(filePathName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
		var streamWriter = new StreamWriter(file);
		streamWriter.AutoFlush = true;
		file.SetLength(0);
		streamWriter.WriteLine(json);

		streamWriter.Close();
	}

	private static async Task SerializeToFileAsync(string filePathName, string json)
	{
		//File.WriteAllText(filePathName, json);
		Directory.CreateDirectory(Path.GetDirectoryName(filePathName));
		var file = File.Open(filePathName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
		var streamWriter = new StreamWriter(file);
		streamWriter.AutoFlush = true;
		file.SetLength(0);
		await streamWriter.WriteLineAsync(json);

		streamWriter.Close();
	}

	public static void SearializeToFile(string filePathName, object obj)
	{
		SerializeToFile(filePathName, Serialize(obj));
	}

	private static async Task SearializeToFileAsync(string filePathName, object obj)
	{
		await SerializeToFileAsync(filePathName, Serialize(obj));
	}

	public static string ReadFromFile(string filePathName)
	{
		//return File.ReadAllText(filePathName);

		var file = File.Open(filePathName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		var streamReader = new StreamReader(file);
		var content = streamReader.ReadToEnd();

		streamReader.Close();

		return content;
	}

	private static async Task<string> ReadFromFileAsync(string filePathName)
	{
		//return File.ReadAllText(filePathName);

		var file = File.Open(filePathName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		var streamReader = new StreamReader(file);
		var content = await streamReader.ReadToEndAsync();

		streamReader.Close();

		return content;
	}
}
