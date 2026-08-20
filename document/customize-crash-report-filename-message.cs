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

            // Set a custom message to be included in the crash report
            options.CustomMessage = "Additional context: processing file XYZ.pdf";

            // Optionally customize the crash report file name
            options.CrashReportFilename = "MyCrashReport.html";

            // Generate the crash report
            PdfException.GenerateCrashReport(options);

            // Output the full path of the generated report
            Console.WriteLine("Crash report generated at: " + options.CrashReportPath);
        }
    }

    static void ThrowSampleException()
    {
        // Example exception (divide by zero)
        int zero = 0;
        int result = 1 / zero;
    }
}