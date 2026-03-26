using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Network share where the crash report will be saved
        string networkShare = "\\\\myserver\\share\\crashreports";

        // Verify that the network share directory exists
        if (!Directory.Exists(networkShare))
        {
            Console.Error.WriteLine($"Network share directory does not exist: {networkShare}");
            return;
        }

        try
        {
            // Simulate an exception to generate a crash report
            throw new InvalidOperationException("Test crash for report generation");
        }
        catch (Exception ex)
        {
            // Create crash report options based on the caught exception
            CrashReportOptions options = new CrashReportOptions(ex);
            // Set the output directory to the network share
            options.CrashReportDirectory = networkShare;
            // Optionally set a custom filename for easier identification
            options.CrashReportFilename = "test_crash_report.html";

            // Generate the crash report HTML file
            PdfException.GenerateCrashReport(options);

            // Verify that the report was created at the expected location
            string reportPath = options.CrashReportPath;
            if (File.Exists(reportPath))
            {
                Console.WriteLine($"Crash report successfully generated at: {reportPath}");
            }
            else
            {
                Console.Error.WriteLine($"Failed to generate crash report at: {reportPath}");
            }
        }
    }
}
