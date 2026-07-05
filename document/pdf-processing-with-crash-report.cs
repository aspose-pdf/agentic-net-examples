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
            // Load, process, and save the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Example processing: add a blank page
                doc.Pages.Add();

                // Save the modified document
                doc.Save(outputPath);
            }
        }
        catch (PdfException pdfEx)
        {
            // Generate a detailed crash report for Aspose.Pdf exceptions
            var crashOptions = new CrashReportOptions(pdfEx)
            {
                CrashReportDirectory = Path.GetDirectoryName(outputPath) ?? Directory.GetCurrentDirectory(),
                CrashReportFilename = "crash_report.html",
                CustomMessage = "Aspose.Pdf exception occurred."
            };
            PdfException.GenerateCrashReport(crashOptions);

            Console.Error.WriteLine($"PdfException: {pdfEx.Message}");
        }
        catch (Exception ex)
        {
            // Generate a crash report for any other unexpected errors
            var crashOptions = new CrashReportOptions(ex)
            {
                CrashReportDirectory = Path.GetDirectoryName(outputPath) ?? Directory.GetCurrentDirectory(),
                CrashReportFilename = "crash_report.html",
                CustomMessage = "Unexpected error during PDF processing."
            };
            PdfException.GenerateCrashReport(crashOptions);

            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}