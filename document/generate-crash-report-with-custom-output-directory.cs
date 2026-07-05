using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define the directory where crash reports will be saved
        string reportDir = Path.Combine(Directory.GetCurrentDirectory(), "CrashReports");
        Directory.CreateDirectory(reportDir); // Ensure the directory exists

        try
        {
            // Simulate an exception that we want to report
            throw new InvalidOperationException("Simulated exception for crash report.");
        }
        catch (Exception ex)
        {
            // Create CrashReportOptions based on the caught exception and configure output
            var options = new CrashReportOptions(ex)
            {
                CrashReportDirectory = reportDir,
                CrashReportFilename = "CrashReport.html",
                CustomMessage = "Additional context for the crash report."
            };

            // Generate the crash report HTML file
            PdfException.GenerateCrashReport(options);

            // Verify that the report file was created
            string reportPath = Path.Combine(options.CrashReportDirectory, options.CrashReportFilename);
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
