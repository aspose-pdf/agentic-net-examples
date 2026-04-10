using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string crashReportExe = "AsposePdfApi.exe"; // Aspose crash‑report helper

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Example processing: add a blank page
                doc.Pages.Add();

                // Save the modified PDF
                doc.Save(outputPath);
                Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
            }
        }
        catch (PdfException pdfEx)
        {
            // Generate a detailed crash report for Aspose.Pdf exceptions, but only if the helper exe is present
            if (File.Exists(crashReportExe))
            {
                var crashOptions = new CrashReportOptions(pdfEx)
                {
                    CrashReportDirectory = Path.GetDirectoryName(outputPath) ?? Directory.GetCurrentDirectory(),
                    CrashReportFilename = "pdf_crash_report.html",
                    CustomMessage = "Aspose.Pdf exception during processing."
                };
                PdfException.GenerateCrashReport(crashOptions);
            }
            else
            {
                Console.Error.WriteLine($"Crash report tool not found ('{crashReportExe}'). Skipping report generation.");
            }
            Console.Error.WriteLine($"PdfException: {pdfEx.Message}");
        }
        catch (Exception ex)
        {
            // Generate a crash report for any other unexpected errors, guarded by the same exe check
            if (File.Exists(crashReportExe))
            {
                var crashOptions = new CrashReportOptions(ex)
                {
                    CrashReportDirectory = Path.GetDirectoryName(outputPath) ?? Directory.GetCurrentDirectory(),
                    CrashReportFilename = "pdf_crash_report.html",
                    CustomMessage = "Unexpected error during PDF processing."
                };
                PdfException.GenerateCrashReport(crashOptions);
            }
            else
            {
                Console.Error.WriteLine($"Crash report tool not found ('{crashReportExe}'). Skipping report generation.");
            }
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
