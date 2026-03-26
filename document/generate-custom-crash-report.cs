using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        try
        {
            // Simulate an error that will be reported
            throw new InvalidOperationException("Sample exception for crash report.");
        }
        catch (Exception ex)
        {
            // Create crash report options based on the caught exception
            CrashReportOptions options = new CrashReportOptions(ex);
            // Include a custom message in the report
            options.CustomMessage = "Additional context: processing step XYZ failed.";
            // Set a custom file name for the generated report
            options.CrashReportFilename = "MyCrashReport.html";
            // Generate the HTML crash report
            PdfException.GenerateCrashReport(options);
            // Show where the report was saved
            Console.WriteLine("Crash report generated at: " + options.CrashReportPath);
        }
    }
}