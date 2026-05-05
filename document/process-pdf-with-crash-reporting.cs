using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        // Use a distinct output file name and folder to avoid locking the project directory
        string outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDirectory);
        string outputPath = Path.Combine(outputDirectory, "output.pdf");

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Example processing: add a blank page
                doc.Pages.Add();

                // Save the modified document to the distinct output location
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
                CustomMessage = "Aspose.Pdf exception during processing."
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
