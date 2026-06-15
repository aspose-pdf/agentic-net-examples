using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        try
        {
            // Simulate an exception (division by zero)
            int zero = 0;
            int _ = 1 / zero;
        }
        catch (Exception ex)
        {
            // Create crash report options based on the caught exception
            CrashReportOptions options = new CrashReportOptions(ex);

            // Optional: specify output directory and filename for the HTML report
            string outputDir = Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location);
            options.CrashReportDirectory = outputDir;
            options.CrashReportFilename = "MyCrashReport.html";

            // Generate the crash report (HTML format) using the built‑in utility
            PdfException.GenerateCrashReport(options);

            // Path to the generated HTML report
            string htmlPath = options.CrashReportPath;

            // Define the PDF output path (same folder, same base name, .pdf extension)
            string pdfPath = Path.ChangeExtension(htmlPath, ".pdf");

            // Load the HTML report and save it as PDF
            using (Document doc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                doc.Save(pdfPath);
            }

            Console.WriteLine($"Crash report PDF created at: {pdfPath}");
        }
    }
}