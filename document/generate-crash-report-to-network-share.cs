using System;
using System.IO;
using Aspose.Pdf;

class CrashReportDemo
{
    static void Main()
    {
        // Simulate an exception that we want to generate a crash report for
        Exception simulatedException = new InvalidOperationException("Simulated exception for crash report.");

        // Create CrashReportOptions with the exception
        CrashReportOptions options = new CrashReportOptions(simulatedException);

        // ---------------------------------------------------------------------
        // Define the network share (UNC) path. In a real scenario this could be
        // read from a configuration file or environment variable.
        // ---------------------------------------------------------------------
        string networkSharePath = @"\\MyServer\Shared\CrashReports";

        // Try to create the directory on the network share. If the share is not
        // reachable (IOException) fall back to a local temporary folder so the
        // demo can still run without throwing an unhandled exception.
        string targetPath = networkSharePath;
        try
        {
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }
        }
        catch (IOException)
        {
            // Network path not found – use a local fallback directory.
            targetPath = Path.Combine(Path.GetTempPath(), "CrashReports");
            Directory.CreateDirectory(targetPath);
        }

        // Set the output directory for the crash report.
        options.CrashReportDirectory = targetPath;

        // Optionally set a custom filename (otherwise it is auto‑generated)
        options.CrashReportFilename = "MyCrashReport.html";

        // Generate the crash report
        PdfException.GenerateCrashReport(options);

        // Verify that the report file was created at the expected location
        string reportPath = options.CrashReportPath; // Full path including filename
        if (File.Exists(reportPath))
        {
            Console.WriteLine($"Crash report successfully written to: {reportPath}");
        }
        else
        {
            Console.WriteLine($"Failed to write crash report to: {reportPath}");
        }
    }
}
