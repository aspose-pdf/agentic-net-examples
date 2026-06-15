using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        try
        {
            // Simulate an operation that throws an exception
            ThrowSampleException();
        }
        catch (Exception ex)
        {
            // Create CrashReportOptions based on the caught exception
            CrashReportOptions options = new CrashReportOptions(ex);

            // Set a custom message – you can include any details you want,
            // such as a desired file name or diagnostic information.
            options.CustomMessage = "Custom crash report for troubleshooting. Desired file name: MyCrashReport.html";

            // Optionally, you can also set the actual file name directly
            // (overrides the auto‑generated name).
            options.CrashReportFilename = "MyCrashReport.html";

            // Generate the HTML crash report
            PdfException.GenerateCrashReport(options);

            // The full path of the generated report is available via CrashReportPath
            Console.WriteLine("Crash report generated at: " + options.CrashReportPath);
        }
    }

    static void ThrowSampleException()
    {
        // This method deliberately throws an exception to demonstrate crash reporting
        throw new InvalidOperationException("Sample exception for crash report generation.");
    }
}