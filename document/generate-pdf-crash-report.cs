using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        try
        {
            // Attempt an operation that may throw a PdfException.
            // Here we try to load a non‑existent PDF file.
            using (Document doc = new Document("nonexistent.pdf"))
            {
                // If the file existed, you could work with the document here.
                Console.WriteLine($"Pages: {doc.Pages.Count}");
            }
        }
        catch (PdfException pdfEx)
        {
            // Create crash‑report options based on the caught exception.
            CrashReportOptions options = new CrashReportOptions(pdfEx)
            {
                // Optional: specify a custom directory or filename.
                // CrashReportDirectory = "C:\\CrashReports",
                // CrashReportFilename = "MyCrashReport.html",
                // CustomMessage = "Additional context for the crash report."
            };

            // Generate the HTML crash report.
            PdfException.GenerateCrashReport(options);

            Console.WriteLine($"Crash report generated at: {options.CrashReportPath}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected exceptions.
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}