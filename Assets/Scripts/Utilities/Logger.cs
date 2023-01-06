using Newtonsoft.Json;

public static class Logger
{
	public static void Print(object message)
	{
		UnityEngine.Debug.Log(message);
	}

	/// <summary>
	/// Allows viewing of full object by JSON serializing the object before printing it.
	/// </summary>
	public static void PrintProps(object message, bool pretty = false)
	{
		string toLog;
		if (pretty)
		{
			toLog = JsonConvert.SerializeObject(message, Formatting.Indented);
		}
		else
		{
			toLog = JsonConvert.SerializeObject(message);
		}
		UnityEngine.Debug.Log(toLog);
	}
}