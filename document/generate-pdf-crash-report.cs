using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string crashReportPath = "crash_report.txt";

        try
        {
            // Load a PDF document – this may throw a PdfException
            using (Document doc = new Document(inputPath))
            {
                // Example operation that could fail
                Console.WriteLine($"Number of pages: {doc.Pages.Count}");
            }
        }
        catch (PdfException ex)
        {
            // Create and configure crash report options using the constructor that accepts the exception
            var options = new CrashReportOptions(ex)
            {
                // Directory where the report will be written (fallback to current directory if null)
                CrashReportDirectory = Path.GetDirectoryName(crashReportPath) ?? Directory.GetCurrentDirectory(),
                // File name of the crash report (extension can be .html, .txt, etc.)
                CrashReportFilename = Path.GetFileName(crashReportPath),
                // Optional custom message to include in the report
                CustomMessage = "Unexpected error while processing the PDF document."
            };

            // Generate the crash report using the static helper on PdfException
            PdfException.GenerateCrashReport(options);

            Console.WriteLine($"Crash report generated at: {Path.Combine(options.CrashReportDirectory, options.CrashReportFilename)}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected exceptions
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
