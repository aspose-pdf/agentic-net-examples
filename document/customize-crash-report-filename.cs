using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        try
        {
            // Example code that throws an exception (divide by zero)
            int zero = 0;
            int result = 10 / zero;
        }
        catch (Exception ex)
        {
            // Create CrashReportOptions based on the caught exception
            CrashReportOptions options = new CrashReportOptions(ex);

            // Set a custom message – you can embed the desired file name here
            options.CustomMessage = "An unexpected error occurred. Desired report file: MyCrashReport.html";

            // Directly set the crash report file name (optional, but demonstrates customization)
            options.CrashReportFilename = "MyCrashReport.html";

            // Generate the HTML crash report
            PdfException.GenerateCrashReport(options);

            // Output the full path of the generated report
            Console.WriteLine($"Crash report generated at: {options.CrashReportPath}");
        }
    }
}