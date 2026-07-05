using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        try
        {
            // Simulate an error that we want to report
            throw new InvalidOperationException("Demo exception for crash report");
        }
        catch (Exception ex)
        {
            // Create crash‑report options based on the caught exception
            CrashReportOptions options = new CrashReportOptions(ex);

            // Set a custom message that will be included in the HTML report
            options.CustomMessage = "Additional context: processing step X failed.";

            // Optionally override the default file name
            options.CrashReportFilename = "MyCrashReport.html";

            // Generate the crash report
            PdfException.GenerateCrashReport(options);

            // Show where the report was saved
            Console.WriteLine("Crash report generated at: " + options.CrashReportPath);
        }
    }
}