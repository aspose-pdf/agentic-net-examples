using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string reportDir = "CrashReports";

        // Ensure the directory for the crash report exists
        Directory.CreateDirectory(reportDir);

        try
        {
            // Load the PDF (wrapped in using for proper disposal)
            using (Document doc = new Document(inputPath))
            {
                // Simulate a complex operation that may fail
                // Here we deliberately access a non‑existent page to trigger an exception
                var invalidPage = doc.Pages[doc.Pages.Count + 1];
            }
        }
        catch (Exception ex)
        {
            // Prepare crash report options with a custom diagnostic message
            CrashReportOptions options = new CrashReportOptions(ex)
            {
                CrashReportDirectory = reportDir,
                CustomMessage = "Error during PDF processing: attempted to access a page beyond the document range."
            };

            // Generate the HTML crash report
            PdfException.GenerateCrashReport(options);

            Console.WriteLine($"Crash report created at: {options.CrashReportPath}");
        }
    }
}