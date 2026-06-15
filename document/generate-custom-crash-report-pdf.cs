using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        try
        {
            // Simulate a PDF operation that throws an exception.
            // Attempt to open a non‑existent PDF file to trigger an error.
            using (Document doc = new Document("nonexistent.pdf"))
            {
                // Force loading of the first page (will not be reached).
                var page = doc.Pages[1];
            }
        }
        catch (Exception ex)
        {
            // Ensure we have a PdfException instance (wrap if necessary).
            PdfException pdfEx = ex as PdfException ?? new PdfException("PDF operation failed", ex);

            // Create crash report options based on the exception.
            CrashReportOptions options = new CrashReportOptions(pdfEx);

            // Include a custom message with additional debugging details.
            options.CustomMessage = $"Custom debug info: Operation failed at {DateTime.Now}\nStackTrace:\n{ex.StackTrace}";

            // Specify where the crash report HTML file should be written.
            options.CrashReportDirectory = Directory.GetCurrentDirectory();
            options.CrashReportFilename = "MyPdfCrashReport.html";

            // Generate the crash report (HTML format) using Aspose.Pdf.
            PdfException.GenerateCrashReport(options);
        }
    }
}