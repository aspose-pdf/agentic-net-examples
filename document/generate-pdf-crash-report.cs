using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        try
        {
            // Simulate an exception (divide by zero)
            int zero = 0;
            int _ = 1 / zero;
        }
        catch (Exception ex)
        {
            // Prepare crash report options
            CrashReportOptions options = new CrashReportOptions(ex)
            {
                // Optional: add a custom message
                CustomMessage = "An unexpected error occurred while processing the PDF.",
                // Optional: specify output directory (defaults to current directory)
                CrashReportDirectory = Directory.GetCurrentDirectory(),
                // Optional: set a specific filename for the intermediate HTML report
                CrashReportFilename = "MyCrashReport.html"
            };

            // Generate the crash report in HTML format
            PdfException.GenerateCrashReport(options);

            // Path to the generated HTML report
            string htmlPath = options.CrashReportPath;

            // Desired final PDF path
            string pdfPath = Path.Combine(Directory.GetCurrentDirectory(), "CrashReport.pdf");

            // Load the HTML report and save it as PDF
            using (Document doc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                doc.Save(pdfPath);
            }

            Console.WriteLine($"Crash report PDF saved to '{pdfPath}'.");
        }
    }
}