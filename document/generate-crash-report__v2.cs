using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Simulate an exception to generate a crash report
        try
        {
            int zero = 0;
            int result = 10 / zero; // This will throw DivideByZeroException
        }
        catch (Exception ex)
        {
            // Build an absolute path for the crash‑report directory and ensure it exists
            string reportDirectory = Path.Combine(Directory.GetCurrentDirectory(), "CrashReports");
            Directory.CreateDirectory(reportDirectory); // safe – creates if missing

            // Create CrashReportOptions with the caught exception and the target directory
            CrashReportOptions options = new CrashReportOptions(ex)
            {
                CrashReportDirectory = reportDirectory,
                // Correct property name is CrashReportFilename (not CrashReportFileName)
                CrashReportFilename = "CrashReport.html",
                CustomMessage = "Demo of generating a crash report with a custom directory."
            };

            // Generate the crash‑report HTML file
            PdfException.GenerateCrashReport(options);

            // Verify that the report file was created
            string reportPath = options.CrashReportPath; // full path returned by the API
            if (File.Exists(reportPath))
            {
                Console.WriteLine($"Crash report successfully generated at: {reportPath}");
            }
            else
            {
                Console.WriteLine("Failed to generate crash report.");
            }
        }
    }
}