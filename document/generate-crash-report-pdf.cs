using System;
using System.IO;
using Aspose.Pdf; // Provides Document, HtmlLoadOptions, CrashReportOptions, PdfException

class CrashReportGenerator
{
    static void Main()
    {
        // Path for the generated PDF report
        const string outputPdfPath = "CrashReport.pdf";

        try
        {
            // Example code that throws an exception
            int zero = 0;
            int _ = 1 / zero; // Triggers DivideByZeroException
        }
        catch (Exception ex)
        {
            // ---------------------------------------------------------------
            // 1. Build crash‑report options based on the caught exception
            // ---------------------------------------------------------------
            CrashReportOptions reportOptions = new CrashReportOptions(ex)
            {
                // Store the HTML report in the same folder as the final PDF
                CrashReportDirectory = Path.GetDirectoryName(Path.GetFullPath(outputPdfPath)),
                // Optional: give the HTML file a friendly name
                CrashReportFilename = "CrashReport.html",
                // Optional: add any custom message you want to appear in the report
                CustomMessage = "An unexpected error occurred while processing the document."
            };

            // ---------------------------------------------------------------
            // 2. Generate the HTML crash report (static utility method)
            // ---------------------------------------------------------------
            PdfException.GenerateCrashReport(reportOptions);

            // ---------------------------------------------------------------
            // 3. Load the generated HTML and save it as a PDF
            // ---------------------------------------------------------------
            string htmlReportPath = reportOptions.CrashReportPath; // full path to the HTML file

            // Load the HTML using the appropriate load options (required for HTML input)
            using (Document htmlDoc = new Document(htmlReportPath, new HtmlLoadOptions()))
            {
                // Save as PDF – the default format is PDF
                htmlDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Crash report PDF created at: {Path.GetFullPath(outputPdfPath)}");
        }
    }
}
