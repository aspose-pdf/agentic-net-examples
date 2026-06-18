using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        try
        {
            // Load the PDF document (using statement ensures proper disposal)
            using (Document doc = new Document(inputPath))
            {
                // Example processing – output the number of pages
                Console.WriteLine($"Pages: {doc.Pages.Count}");

                // Save the (potentially modified) document
                doc.Save(outputPath);
            }
        }
        catch (PdfException pdfEx) // Handles Aspose.Pdf specific exceptions
        {
            Console.Error.WriteLine($"PdfException: {pdfEx.Message}");

            // Generate a detailed crash report for the PDF‑specific error
            var crashOptions = new CrashReportOptions(pdfEx)
            {
                CrashReportDirectory = Path.GetDirectoryName(outputPath) ?? Directory.GetCurrentDirectory(),
                CrashReportFilename = "crash_report.html",
                CustomMessage = "A PDF‑specific error occurred during processing."
            };
            PdfException.GenerateCrashReport(crashOptions);
        }
        catch (Exception ex) // Handles any other unexpected exceptions
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");

            // Generate a detailed crash report for the unexpected error
            var crashOptions = new CrashReportOptions(ex)
            {
                CrashReportDirectory = Path.GetDirectoryName(outputPath) ?? Directory.GetCurrentDirectory(),
                CrashReportFilename = "crash_report.html",
                CustomMessage = "Unexpected error during PDF processing."
            };
            PdfException.GenerateCrashReport(crashOptions);
        }
    }
}
