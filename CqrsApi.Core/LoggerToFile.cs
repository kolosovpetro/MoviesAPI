using System;
using System.IO;

namespace CqrsApi.Core;

public static class LoggerToFile
{
    private const string LogFile = "Logger.txt";

    public static void PrintExceptionToFile(Exception e)
    {
        using var sw = File.AppendText(LogFile);
        sw.WriteLine(DateTime.UtcNow);
        sw.WriteLine(e.Message);
        sw.WriteLine(e.StackTrace);
    }

    public static void PrintToLog(string message)
    {
        using var sw = File.AppendText(LogFile);
        sw.WriteLine(DateTime.UtcNow);
        sw.WriteLine(message);
    }
}