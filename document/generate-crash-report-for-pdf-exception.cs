using System;
using System.IO;
using Aspose.Pdf; // Provides PdfException and CrashReportOptions

class Program
{
    static void Main()
    {
        try
        {
            // Example operation that may throw a PdfException.
            // Attempt to open a non‑existent PDF file.
            using (Document doc = new Document("nonexistent.pdf"))
            {
                // No further processing needed; the exception will be thrown on load.
            }
        }
        catch (PdfException pdfEx) // Catch the specific Aspose.Pdf exception.
        {
            // Create crash report options based on the caught exception.
            CrashReportOptions options = new CrashReportOptions(pdfEx);

            // Optional: customize output directory and filename.
            // By default the directory is the current working directory.
            options.CrashReportDirectory = Path.Combine(Environment.CurrentDirectory, "CrashReports");
            // Ensure the directory exists.
            Directory.CreateDirectory(options.CrashReportDirectory);

            // Generate the HTML crash report.
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