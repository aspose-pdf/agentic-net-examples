using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define the directory where the crash report will be saved
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "CrashReports");
        Directory.CreateDirectory(outputDir);

        try
        {
            // Simulate an exception that we want to report
            throw new InvalidOperationException("Sample exception for crash report generation.");
        }
        catch (Exception ex)
        {
            // Create CrashReportOptions based on the caught exception
            CrashReportOptions options = new CrashReportOptions(ex);

            // Set the desired output directory for the report
            options.CrashReportDirectory = outputDir;

            // Optionally set a custom filename (otherwise it is auto‑generated)
            options.CrashReportFilename = "MyCrashReport.html";

            // Generate the HTML crash report
            PdfException.GenerateCrashReport(options);

            // Verify that the report file was created
            string reportPath = options.CrashReportPath;
            if (File.Exists(reportPath))
            {
                Console.WriteLine($"Crash report successfully generated at: {reportPath}");
            }
            else
            {
                Console.WriteLine("Crash report generation failed.");
            }
        }
    }
}