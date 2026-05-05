using System;
using System.IO;
using Aspose.Pdf;

class CrashReportDemo
{
    static void Main()
    {
        // Path to a network share (UNC). Adjust as needed for your environment.
        const string networkSharePath = @"\\MyServer\Shared\CrashReports";

        // Ensure the directory exists; create it if it does not.
        if (!Directory.Exists(networkSharePath))
        {
            Directory.CreateDirectory(networkSharePath);
        }

        // Simulate an exception to generate a crash report.
        Exception simulatedException;
        try
        {
            // Example: divide by zero to cause an exception.
            int zero = 0;
            int result = 10 / zero;
            result++; // never reached
            simulatedException = null; // placeholder
        }
        catch (Exception ex)
        {
            simulatedException = ex;
        }

        // Create CrashReportOptions with the captured exception.
        CrashReportOptions options = new CrashReportOptions(simulatedException);

        // Set the output directory to the network share.
        options.CrashReportDirectory = networkSharePath;

        // Optionally set a custom filename.
        options.CrashReportFilename = "MyCrashReport.html";

        // Generate the crash report.
        PdfException.GenerateCrashReport(options);

        // Verify that the report file was created at the expected location.
        string reportPath = options.CrashReportPath;
        if (File.Exists(reportPath))
        {
            Console.WriteLine($"Crash report successfully written to: {reportPath}");
        }
        else
        {
            Console.WriteLine($"Failed to write crash report. Expected location: {reportPath}");
        }
    }
}