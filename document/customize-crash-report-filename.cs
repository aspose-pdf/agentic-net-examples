using System;
using Aspose.Pdf; // Provides CrashReportOptions and PdfException

class Program
{
    static void Main()
    {
        try
        {
            // Simulate an operation that throws an exception
            throw new InvalidOperationException("Sample error for crash report generation");
        }
        catch (Exception ex)
        {
            // Create CrashReportOptions based on the caught exception
            CrashReportOptions options = new CrashReportOptions(ex);

            // Set a custom message that will be included in the HTML report
            options.CustomMessage = "Additional context: processing file 'Sample.pdf' with user ID 12345";

            // (Optional) Override the default report filename
            options.CrashReportFilename = "CustomCrashReport.html";

            // Generate the crash report HTML file
            PdfException.GenerateCrashReport(options);

            // Inform the user where the report was saved
            Console.WriteLine("Crash report generated at: " + options.CrashReportPath);
        }
    }
}