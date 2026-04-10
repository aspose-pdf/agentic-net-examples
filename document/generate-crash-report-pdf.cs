using System;
using System.IO;
using Aspose.Pdf; // HtmlLoadOptions, CrashReportOptions, PdfException, Document are all in this namespace

class CrashReportGenerator
{
    static void Main()
    {
        const string outputPdf = "CrashReport.pdf";

        try
        {
            // Simulate an exception to demonstrate crash report generation
            int zero = 0;
            int _ = 1 / zero;
        }
        catch (Exception ex)
        {
            // Create crash report options based on the caught exception
            CrashReportOptions options = new CrashReportOptions(ex)
            {
                // Optional: customize output directory and filename
                CrashReportDirectory = Directory.GetCurrentDirectory(),
                CrashReportFilename   = "MyCrashReport.html",
                CustomMessage         = "An unexpected error occurred while processing the document."
            };

            // Generate the HTML crash report
            PdfException.GenerateCrashReport(options);

            // Convert the generated HTML report to PDF
            try
            {
                using (Document htmlDoc = new Document(options.CrashReportPath, new HtmlLoadOptions()))
                {
                    // Save as PDF (default format)
                    htmlDoc.Save(outputPdf);
                }

                Console.WriteLine($"Crash report PDF saved to '{outputPdf}'.");
            }
            catch (TypeInitializationException)
            {
                // HTML conversion requires GDI+ (Windows only)
                Console.WriteLine("HTML to PDF conversion is not supported on this platform.");
            }
        }
    }
}
