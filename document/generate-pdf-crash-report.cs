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
            // Attempt to load a PDF document – this may throw PdfException
            using (Document doc = new Document(inputPath))
            {
                // Perform any processing here
                Console.WriteLine($"Loaded PDF with {doc.Pages.Count} pages.");
            }
        }
        catch (PdfException ex)
        {
            // Create crash report options using the constructor that requires the exception
            var crashOptions = new CrashReportOptions(ex)
            {
                // Directory where the report will be written
                CrashReportDirectory = Path.GetDirectoryName(crashReportPath) ?? Directory.GetCurrentDirectory(),
                // File name only (no path) – the directory above is used for the full path
                CrashReportFilename = Path.GetFileName(crashReportPath),
                // Optional custom message to include in the report
                CustomMessage = "Error loading PDF document."
            };

            // Generate the crash report file
            PdfException.GenerateCrashReport(crashOptions);

            Console.WriteLine($"Crash report generated at '{crashReportPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected exceptions
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}