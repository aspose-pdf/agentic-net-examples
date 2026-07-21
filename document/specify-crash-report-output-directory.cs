using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Simulate an exception to generate a crash report for
        Exception capturedException = null;
        try
        {
            // This will throw DivideByZeroException
            int zero = 0;
            int result = 1 / zero;
        }
        catch (Exception ex)
        {
            capturedException = ex;
        }

        if (capturedException == null)
        {
            Console.WriteLine("No exception captured; nothing to report.");
            return;
        }

        // Create CrashReportOptions using the captured exception
        CrashReportOptions options = new CrashReportOptions(capturedException);

        // Define an output directory for the crash report
        string outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "CrashReports");
        Directory.CreateDirectory(outputDirectory); // Ensure the directory exists

        // Set the desired output directory
        options.CrashReportDirectory = outputDirectory;

        // Optionally customize the filename and add a custom message
        options.CrashReportFilename = "MyCrashReport.html";
        options.CustomMessage = "Additional context: test run for crash report generation.";

        // Generate the crash report
        PdfException.GenerateCrashReport(options);

        // Verify that the report was saved correctly
        string reportPath = options.CrashReportPath;
        if (File.Exists(reportPath))
        {
            Console.WriteLine($"Crash report successfully generated at: {reportPath}");
        }
        else
        {
            Console.WriteLine("Failed to generate the crash report.");
        }
    }
}