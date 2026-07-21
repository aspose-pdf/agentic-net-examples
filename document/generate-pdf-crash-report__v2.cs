using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "nonexistent.pdf";          // Path that will cause an error
        const string reportDirectory = "CrashReports";       // Where to store the report

        try
        {
            // Attempt to load a PDF – this will throw because the file does not exist
            using (Document doc = new Document(inputPath))
            {
                Console.WriteLine($"Pages: {doc.Pages.Count}");
            }
        }
        catch (Exception ex)
        {
            // Ensure the directory for the crash report exists
            Directory.CreateDirectory(reportDirectory);

            // Create crash‑report options based on the caught exception
            CrashReportOptions options = new CrashReportOptions(ex)
            {
                CrashReportDirectory = reportDirectory,
                CrashReportFilename = "MyCrashReport.html",    // Custom file name (optional)
                CustomMessage = $"Error while processing '{inputPath}'."
            };

            // Generate the HTML crash report
            PdfException.GenerateCrashReport(options);

            Console.WriteLine($"Crash report generated at: {options.CrashReportPath}");
        }
    }
}