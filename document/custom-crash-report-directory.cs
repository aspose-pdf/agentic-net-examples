using System;
using System.IO;
using Aspose.Pdf;   // CrashReportOptions and PdfException are in this namespace

class Program
{
    static void Main()
    {
        // Simulate an operation that throws an exception
        try
        {
            // Example: divide by zero to generate an exception
            int zero = 0;
            int _ = 1 / zero;
        }
        catch (Exception ex)
        {
            // Define the directory where the crash report will be saved
            string reportDir = Path.Combine(Directory.GetCurrentDirectory(), "CrashReports");
            Directory.CreateDirectory(reportDir); // Ensure the directory exists

            // Create CrashReportOptions for the caught exception
            CrashReportOptions options = new CrashReportOptions(ex)
            {
                CrashReportDirectory = reportDir,          // Set custom output directory
                CrashReportFilename = "MyCrashReport.html",// Optional: custom file name
                CustomMessage = "Additional context for debugging." // Optional custom message
            };

            // Generate the crash report (HTML format)
            PdfException.GenerateCrashReport(options);

            // Verify that the report file was created
            string reportPath = options.CrashReportPath;
            if (File.Exists(reportPath))
            {
                Console.WriteLine($"Crash report successfully generated at: {reportPath}");
            }
            else
            {
                Console.WriteLine("Failed to generate the crash report.");
            }
        }
    }
}