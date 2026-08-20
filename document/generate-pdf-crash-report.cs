using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        try
        {
            // Example operation that may throw a PdfException.
            // Replace with actual PDF processing logic as needed.
            using (Document doc = new Document("nonexistent.pdf"))
            {
                Console.WriteLine($"Pages: {doc.Pages.Count}");
            }
        }
        catch (PdfException ex)
        {
            // Create crash report options based on the caught exception.
            CrashReportOptions options = new CrashReportOptions(ex)
            {
                // Optional customizations:
                CustomMessage = "An error occurred while processing the PDF document."
                // CrashReportDirectory = "C:\\CrashReports";
                // CrashReportFilename = "MyCrashReport.html";
            };

            // Generate the crash report HTML file.
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