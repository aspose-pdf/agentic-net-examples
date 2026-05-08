using System;
using System.IO;
using Aspose.Pdf; // Contains Document, PdfException, CrashReportOptions

class Program
{
    static void Main()
    {
        try
        {
            // Attempt an operation that may throw a PdfException.
            // Here we try to load a non‑existent PDF to trigger the exception.
            using (Document doc = new Document("nonexistent.pdf"))
            {
                // Example usage (won't be reached if exception occurs)
                Console.WriteLine($"Pages: {doc.Pages.Count}");
            }
        }
        catch (PdfException ex)
        {
            // Create crash report options based on the caught exception.
            CrashReportOptions options = new CrashReportOptions(ex);

            // Optional: customize the report.
            options.CustomMessage = "Error while processing PDF document.";
            options.CrashReportDirectory = Directory.GetCurrentDirectory(); // output folder
            // options.CrashReportFilename can be set if a specific name is desired.

            // Generate the crash report HTML file.
            PdfException.GenerateCrashReport(options);

            // Inform the user where the report was saved.
            Console.WriteLine($"Crash report generated at: {options.CrashReportPath}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected exceptions.
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}