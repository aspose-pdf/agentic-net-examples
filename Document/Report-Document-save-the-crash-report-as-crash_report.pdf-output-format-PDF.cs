using System;
using System.IO;
using Aspose.Pdf;

class CrashReportGenerator
{
    static void Main()
    {
        try
        {
            // Simulate an exception to generate a crash report.
            int zero = 0;
            int _ = 1 / zero;
        }
        catch (Exception ex)
        {
            try
            {
                // Prepare crash report options.
                var crashOptions = new CrashReportOptions(ex)
                {
                    // Save the HTML report in the current directory.
                    CrashReportDirectory = Directory.GetCurrentDirectory(),
                    CrashReportFilename = "crash_report.html"
                };

                // Generate the crash report in HTML format.
                PdfException.GenerateCrashReport(crashOptions);

                // Load the generated HTML report.
                string htmlPath = Path.Combine(crashOptions.CrashReportDirectory, crashOptions.CrashReportFilename);
                if (!File.Exists(htmlPath))
                {
                    Console.WriteLine($"Crash report HTML not found at {htmlPath}");
                    return;
                }
                var pdfDoc = new Document(htmlPath, new HtmlLoadOptions());

                // Save the report as PDF.
                string outputPdf = Path.Combine(Directory.GetCurrentDirectory(), "crash_report.pdf");
                pdfDoc.Save(outputPdf);
                Console.WriteLine($"Crash report saved as PDF: {outputPdf}");
            }
            catch (Exception innerEx)
            {
                Console.WriteLine($"Error generating crash report: {innerEx.Message}");
            }
        }
    }
}