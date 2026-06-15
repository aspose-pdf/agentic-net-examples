using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Network share where the crash report will be saved (UNC path)
        string networkShare = @"\\MyServer\CrashReports";

        // Ensure the directory exists; create it if necessary
        if (!Directory.Exists(networkShare))
        {
            Directory.CreateDirectory(networkShare);
        }

        try
        {
            // Simulate an exception to generate a crash report
            throw new InvalidOperationException("Simulated exception for crash report demo.");
        }
        catch (Exception ex)
        {
            // Create CrashReportOptions based on the caught exception
            CrashReportOptions options = new CrashReportOptions(ex);

            // Set the output directory to the network share
            options.CrashReportDirectory = networkShare;

            // Optional: set a custom filename for the report
            options.CrashReportFilename = "DemoCrashReport.html";

            // Generate the crash report
            PdfException.GenerateCrashReport(options);

            // Verify that the report file was created at the expected location
            string reportPath = options.CrashReportPath;
            if (File.Exists(reportPath))
            {
                Console.WriteLine($"Crash report successfully written to: {reportPath}");
            }
            else
            {
                Console.WriteLine($"Failed to write crash report to: {reportPath}");
            }
        }
    }
}