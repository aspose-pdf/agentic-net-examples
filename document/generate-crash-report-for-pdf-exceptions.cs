using System;
using System.IO;
using Aspose.Pdf;

class CrashReportDemo
{
    static void Main()
    {
        try
        {
            // Example operation that may throw a PdfException.
            // Attempt to load a PDF file that does not exist to trigger an exception.
            using (Document doc = new Document("nonexistent.pdf"))
            {
                // Perform operations on the document (omitted).
            }
        }
        catch (PdfException pdfEx)
        {
            // Create crash report options based on the caught exception.
            CrashReportOptions options = new CrashReportOptions(pdfEx);

            // Optional: customize the output directory and filename.
            options.CrashReportDirectory = Path.Combine(Environment.CurrentDirectory, "CrashReports");
            // Ensure the directory exists.
            Directory.CreateDirectory(options.CrashReportDirectory);

            // Optional: add a custom message with additional context.
            options.CustomMessage = "An error occurred while processing the PDF document.";

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