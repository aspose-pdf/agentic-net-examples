using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "nonexistent.pdf";

        try
        {
            // Attempt to load a PDF that does not exist – this will throw a PdfException
            using (Document doc = new Document(inputPath))
            {
                Console.WriteLine($"Pages: {doc.Pages.Count}");
            }
        }
        catch (PdfException ex)
        {
            // Create crash report options based on the caught exception
            CrashReportOptions options = new CrashReportOptions(ex)
            {
                // Set the directory where the report will be saved (default is current directory)
                CrashReportDirectory = Directory.GetCurrentDirectory(),
                // Provide a custom filename for the report
                CrashReportFilename = "MyCrashReport.html",
                // Optional custom message with additional context
                CustomMessage = "Error occurred while loading PDF."
            };

            // Generate the HTML crash report
            PdfException.GenerateCrashReport(options);

            Console.WriteLine($"Crash report generated at: {options.CrashReportPath}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected exceptions
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}