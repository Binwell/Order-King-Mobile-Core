using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using Newtonsoft.Json;

namespace OrderKingCoreDemo.DAL.Helpers
{
    public static class DataTools
    {
	    public static string EscapeString(string str) {
		    if (!string.IsNullOrEmpty(str)) {
			    str = str.Replace("/", "//");
			    str = str.Replace("_", "/_");
			    str = str.Replace("%", "/%");
			    str = str.Replace("[", "/[");
		    }

		    return str;
	    }

	    public static byte[] GetByteArrayFromStream(Stream stream) {
		    using (var memoryStream = new MemoryStream()) {
			    stream.CopyTo(memoryStream);
			    return memoryStream.ToArray();
		    }
	    }

	    public static List<T> ParseCsv<T>(Stream csvStream, Func<string[],T> convertFunc, params char[] seprators) {
			var result = new List<T>();
		    using (var reader = new StreamReader(csvStream))
			    while (reader.Peek() >= 0)
				    result.Add(convertFunc(reader.ReadLine().Split(seprators)));
		    return result;
	    }

	    public static List<T> ParseCsvWithHeaders<T>(string fileName, params char[] separators) {
		    var result = new List<T>();
		    try {
			    var props = typeof(T).GetRuntimeProperties().ToList();

			    var lines = GetFileLinesContent(fileName);
			    if (lines == null || lines.Count == 0)
				    return null;

			    var titles = lines.FirstOrDefault()?.Split(separators).ToList();
			    if (titles == null)
					return null;

			    foreach (var line in lines.Skip(1)) {
				    var obj = Activator.CreateInstance<T>();
				    var values = line.Split(separators);
				    for (var index = 0; index < titles.Count; index++) {
					    try {
						    var title = titles[index];
						    var prop = props.FirstOrDefault(p => p.Name.ToLower() == title.ToLower());

						    if (prop == null)
							    continue;

						    var value = values[index];

						    if (string.IsNullOrWhiteSpace(value))
							    continue;

						    var propType = prop.PropertyType;

						    try {
							    if (Activator.CreateInstance(propType) is Enum) {
								    var valueForSet = Enum.Parse(propType, value);
								    prop.SetValue(obj, valueForSet);
								    continue;
							    }
						    }
						    catch (Exception e) {
							    // ignored
						    }

						    try {

							    var valueForSet = Convert.ChangeType(value, propType);
							    prop.SetValue(obj, valueForSet);
							    continue;
						    }
						    catch (Exception e) {
							    // ignored
						    }

						    try {
							    var decoded = WebUtility.UrlDecode(value);
							    var readyValue = JsonConvert.DeserializeObject(decoded ?? value, propType);
							    var valueForSet = Convert.ChangeType(readyValue, propType);
							    prop.SetValue(obj, valueForSet);
							    continue;
						    }
						    catch (Exception e) {
						    }
					    }
					    catch (Exception e) {
						    // ignored
					    }
				    }
				    result.Add(obj);
			    }
		    }
		    catch (Exception e) {
			    // ignored
		    }
		    return result;
	    }

	    public static string GetFileContent(string fileName) {
		    string content;
		    var assembly = typeof(DataTools).GetTypeInfo().Assembly;
		    var stream = assembly.GetManifestResourceStream(fileName);
		    if (stream == null) return null;
		    using (var reader = new StreamReader(stream)) content = reader.ReadToEnd();

		    return content;
	    }

	    public static List<string> GetFileLinesContent(string fileName) {
		    var assembly = typeof(DataTools).GetTypeInfo().Assembly;
		    var stream = assembly.GetManifestResourceStream(fileName);
		    if (stream == null) return null;
		    var lines = new List<string>();
		    using (var reader = new StreamReader(stream))
			    while (reader.Peek() >= 0) {
				    lines.Add(reader.ReadLine());
			    }

		    return lines;
	    }
	}
}
