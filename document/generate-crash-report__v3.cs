using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string crashReportFileName = "crash_report.html";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Example processing: add a blank page
                doc.Pages.Add();
                doc.Save(outputPath);
                Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
            }
        }
        catch (PdfException pdfEx)
        {
            Console.Error.WriteLine($"PdfException caught: {pdfEx.Message}");
            GenerateCrashReport(pdfEx, crashReportFileName);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
            GenerateCrashReport(ex, crashReportFileName);
        }
    }

    private static void GenerateCrashReport(Exception ex, string fileName)
    {
        var options = new CrashReportOptions(ex)
        {
            CrashReportDirectory = Directory.GetCurrentDirectory(),
            CrashReportFilename = fileName,
            CustomMessage = "Error occurred during PDF processing."
        };
        PdfException.GenerateCrashReport(options);
        Console.Error.WriteLine($"Crash report generated at '{Path.Combine(options.CrashReportDirectory, options.CrashReportFilename)}'.");
    }
}
