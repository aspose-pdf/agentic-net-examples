using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Network share where the crash report will be saved
        string networkShare = @"\\Server\Share\CrashReports";

        // Verify that the UNC path is reachable; if not, fall back to a local temporary folder
        if (!Directory.Exists(networkShare))
        {
            // Use a local folder (e.g., the system temporary directory) as a safe fallback
            networkShare = Path.Combine(Path.GetTempPath(), "CrashReports");
        }

        // Ensure the directory exists (creates it if missing)
        Directory.CreateDirectory(networkShare);

        // Create a sample exception to generate a report for
        Exception sampleException = new InvalidOperationException("Sample exception for crash report");

        // Initialize CrashReportOptions with the exception
        CrashReportOptions options = new CrashReportOptions(sampleException);

        // Set the output directory to the network share (or the fallback folder)
        // The correct property is CrashReportDirectory.
        options.CrashReportDirectory = networkShare;

        // Optional: specify a custom filename for the report
        options.CrashReportFilename = "MyCrashReport.html";

        // Generate the crash report HTML file
        PdfException.GenerateCrashReport(options);

        // Verify that the report was written to the expected location
        string reportPath = options.CrashReportPath;
        if (File.Exists(reportPath))
        {
            Console.WriteLine($"Crash report successfully written to: {reportPath}");
        }
        else
        {
            Console.WriteLine("Failed to create crash report.");
        }
    }
}
