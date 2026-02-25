using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the generated reports
        const string htmlReportPath = "CrashReport.html";
        const string pdfReportPath  = "CrashReport.pdf";

        // Variable to hold the CrashReportOptions instance
        CrashReportOptions crashOptions = null;

        // Simulate an operation that throws an exception
        try
        {
            int zero = 0;
            int _ = 1 / zero; // This will throw DivideByZeroException
        }
        catch (Exception ex)
        {
            // Create CrashReportOptions with the caught exception
            crashOptions = new CrashReportOptions(ex)
            {
                CrashReportDirectory = Directory.GetCurrentDirectory(),
                CrashReportFilename   = Path.GetFileName(htmlReportPath) // set desired HTML filename
            };

            // Generate the HTML crash report
            PdfException.GenerateCrashReport(crashOptions);
        }

        // If the crash report was generated, convert the HTML to PDF
        if (crashOptions != null && File.Exists(crashOptions.CrashReportPath))
        {
            try
            {
                // Load the HTML report (requires GDI+ on Windows)
                using (Document htmlDoc = new Document(crashOptions.CrashReportPath, new HtmlLoadOptions()))
                {
                    // Save as PDF (no SaveOptions needed for PDF output)
                    htmlDoc.Save(pdfReportPath);
                }

                Console.WriteLine($"Crash report saved as PDF: {pdfReportPath}");
            }
            catch (TypeInitializationException)
            {
                // HTML-to-PDF conversion is Windows‑only (requires GDI+)
                Console.WriteLine("HTML to PDF conversion requires Windows (GDI+). PDF report not generated.");
            }
            catch (Exception convEx)
            {
                Console.Error.WriteLine($"Error converting HTML to PDF: {convEx.Message}");
            }
        }
        else
        {
            Console.WriteLine("No crash report was generated.");
        }
    }
}