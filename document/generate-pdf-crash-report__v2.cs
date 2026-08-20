using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        try
        {
            // Attempt to load a non‑existent PDF to trigger a PdfException.
            using (Document doc = new Document("nonexistent.pdf"))
            {
                // This block will not be reached.
                Console.WriteLine($"Pages: {doc.Pages.Count}");
            }
        }
        catch (PdfException ex)
        {
            // Create crash‑report options based on the caught exception.
            CrashReportOptions options = new CrashReportOptions(ex);

            // Custom message – can include variable values, stack trace fragments, etc.
            options.CustomMessage = "Custom crash report: operation failed while loading a PDF file.\n"
                                 + $"Timestamp: {DateTime.UtcNow:u}\n"
                                 + $"Method: {MethodBase.GetCurrentMethod()?.Name}";

            // Optional: specify output directory and filename.
            string outputDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? ".", "CrashReports");
            Directory.CreateDirectory(outputDir);
            options.CrashReportDirectory = outputDir;
            options.CrashReportFilename = "MyPdfCrashReport.html";

            // Generate the HTML crash report.
            PdfException.GenerateCrashReport(options);

            Console.WriteLine($"Crash report generated at: {options.CrashReportPath}");
        }
        catch (Exception unexpected)
        {
            // Fallback for any other unexpected exceptions.
            Console.Error.WriteLine($"Unexpected error: {unexpected.Message}");
        }
    }
}