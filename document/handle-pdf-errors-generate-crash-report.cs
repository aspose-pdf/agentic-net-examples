using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string crashReportFile = "crash_report.html";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load and process the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Example processing – write page count to console
                Console.WriteLine($"Pages: {doc.Pages.Count}");

                // Save (or re‑save) the document
                doc.Save(outputPath);
                Console.WriteLine($"Document saved to '{outputPath}'.");
            }
        }
        catch (PdfException pdfEx)
        {
            // Handle Aspose.Pdf specific exceptions
            Console.Error.WriteLine($"PdfException: {pdfEx.Message}");

            var crashOptions = new CrashReportOptions(pdfEx)
            {
                CrashReportDirectory = Path.GetDirectoryName(outputPath) ?? Directory.GetCurrentDirectory(),
                CrashReportFilename = crashReportFile,
                CustomMessage = "Aspose.Pdf processing error."
            };

            // Generate a detailed HTML crash report
            PdfException.GenerateCrashReport(crashOptions);
            Console.Error.WriteLine($"Crash report generated at '{Path.Combine(crashOptions.CrashReportDirectory, crashOptions.CrashReportFilename)}'.");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected exceptions
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");

            var crashOptions = new CrashReportOptions(ex)
            {
                CrashReportDirectory = Path.GetDirectoryName(outputPath) ?? Directory.GetCurrentDirectory(),
                CrashReportFilename = crashReportFile,
                CustomMessage = "Unexpected error during PDF processing."
            };

            PdfException.GenerateCrashReport(crashOptions);
            Console.Error.WriteLine($"Crash report generated at '{Path.Combine(crashOptions.CrashReportDirectory, crashOptions.CrashReportFilename)}'.");
        }
    }
}
