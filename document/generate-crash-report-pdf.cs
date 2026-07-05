using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        try
        {
            // Simulate an exception to demonstrate crash report generation
            int zero = 0;
            int _ = 1 / zero;
        }
        catch (Exception ex)
        {
            // Prepare crash report options based on the caught exception
            CrashReportOptions options = new CrashReportOptions(ex);

            // Define output directory and filename for the HTML report
            string reportDir = Path.Combine(Directory.GetCurrentDirectory(), "CrashReports");
            Directory.CreateDirectory(reportDir);
            options.CrashReportDirectory = reportDir;
            options.CrashReportFilename = "MyCrashReport.html";

            // Generate the crash report (HTML format) using the built‑in utility
            PdfException.GenerateCrashReport(options);

            // Convert the generated HTML report to PDF
            string htmlPath = options.CrashReportPath;               // Full path to the HTML file
            string pdfPath  = Path.ChangeExtension(htmlPath, ".pdf"); // Desired PDF output path

            // Load the HTML file with HtmlLoadOptions and save as PDF
            using (Document doc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                // Document.Save(string) without SaveOptions always writes PDF
                doc.Save(pdfPath);
            }

            Console.WriteLine($"Crash report PDF saved to: {pdfPath}");
        }
    }
}