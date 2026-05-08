using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        try
        {
            // Simulate an exception to generate a crash report for
            ThrowDemoException();
        }
        catch (Exception ex)
        {
            // Prepare crash report options
            CrashReportOptions options = new CrashReportOptions(ex)
            {
                // Optional customizations
                CrashReportDirectory = Directory.GetCurrentDirectory(),
                CrashReportFilename = "MyCrashReport.html",
                CustomMessage = "Additional context for the crash."
            };

            // Generate the HTML crash report
            PdfException.GenerateCrashReport(options);

            // Build the full path to the generated HTML report (CrashReportPath property does not exist in some versions)
            string htmlPath = Path.Combine(options.CrashReportDirectory, options.CrashReportFilename);
            string pdfPath = Path.Combine(options.CrashReportDirectory, "MyCrashReport.pdf");

            try
            {
                // Load HTML (requires HtmlLoadOptions) and save as PDF
                using (Document doc = new Document(htmlPath, new HtmlLoadOptions()))
                {
                    doc.Save(pdfPath); // default format is PDF
                }

                Console.WriteLine($"Crash report PDF saved to '{pdfPath}'.");
            }
            catch (TypeInitializationException)
            {
                // HTML conversion relies on GDI+ (Windows only)
                Console.WriteLine("HTML to PDF conversion requires Windows (GDI+). HTML report saved only.");
            }
        }
    }

    static void ThrowDemoException()
    {
        // Example exception
        throw new InvalidOperationException("Demo exception for crash report.");
    }
}
