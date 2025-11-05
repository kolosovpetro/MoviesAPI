using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MoviesAPI.Core;

public static class WindowsEventLogHelper
{
    public static void WriteToWindowsEventLog(
        string message,
        EventLogEntryType type,
        string sourceName,
        string windowsEventLogSection)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return;
        }

        var appLog = new EventLog(windowsEventLogSection);
        var newSourceName = CreateEventSource(windowsEventLogSection, sourceName);
        appLog.Source = newSourceName;

        using var eventLog = new EventLog(windowsEventLogSection);
        eventLog.Source = sourceName;
        eventLog.WriteEntry(message, type);
    }

    public static string CreateEventSource(string windowsEventLogSection, string sourceName)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.WriteLine("Not windows");
            return string.Empty;
        }

        var sourceExists = EventLog.SourceExists(sourceName);

        if (!sourceExists)
        {
            EventLog.CreateEventSource(sourceName, windowsEventLogSection);
        }

        return sourceName;
    }
}