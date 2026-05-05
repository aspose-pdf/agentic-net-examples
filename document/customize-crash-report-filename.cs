using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        try
        {
            // Simulate an operation that throws an exception
            throw new InvalidOperationException("Sample error for crash report generation.");
        }
        catch (Exception ex)
        {
            // Create CrashReportOptions based on the caught exception
            CrashReportOptions options = new CrashReportOptions(ex);

            // Set a custom message to be included in the crash report
            options.CustomMessage = "Custom report for operation XYZ – additional context here.";

            // Optionally, specify a custom filename for the report
            options.CrashReportFilename = "MyCustomCrashReport.html";

            // Generate the HTML crash report
            PdfException.GenerateCrashReport(options);

            // Inform the user where the report was saved
            Console.WriteLine("Crash report generated at: " + options.CrashReportPath);
        }
    }
}