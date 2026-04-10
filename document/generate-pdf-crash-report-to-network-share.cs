using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to an existing PDF (any valid file will do)
        const string pdfPath = "sample.pdf";

        // Network share where the crash report should be written
        const string networkShare = @"\\Server\Share\CrashReports";

        // Verify that the network share is reachable
        if (!Directory.Exists(networkShare))
        {
            Console.Error.WriteLine($"Network directory not found: {networkShare}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for proper disposal
            using (Document doc = new Document(pdfPath))
            {
                // Force an exception (Pages are 1‑based; accessing index 0 throws)
                var invalidPage = doc.Pages[0];
            }
        }
        catch (Exception ex)
        {
            // Create crash‑report options based on the caught exception
            CrashReportOptions options = new CrashReportOptions(ex)
            {
                // Set the output directory to the network share
                CrashReportDirectory = networkShare,
                // Optional: add a custom message
                CustomMessage = "Crash report generated during test execution."
            };

            // Generate the HTML crash report
            PdfException.GenerateCrashReport(options);

            // Verify that the report file was created
            string reportPath = options.CrashReportPath;
            if (File.Exists(reportPath))
            {
                Console.WriteLine($"Crash report successfully written to: {reportPath}");
            }
            else
            {
                Console.Error.WriteLine($"Failed to write crash report to: {reportPath}");
            }
        }
    }
}