using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Simulate an exception that will be used to generate the crash report
        Exception simulatedException = new InvalidOperationException("Simulated exception for crash report.");

        // Create CrashReportOptions with the exception
        CrashReportOptions crashOptions = new CrashReportOptions(simulatedException);

        // Define a network share path (ensure it ends without a trailing backslash)
        string networkSharePath = @"\\Server\Share\CrashReports";
        string targetDirectory = networkSharePath;

        // Try to create the network directory; if it fails, fall back to a local temp folder
        try
        {
            DirectoryInfo di = Directory.CreateDirectory(networkSharePath);
            if (!di.Exists)
                throw new IOException("Directory creation reported success but the directory does not exist.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unable to access network share: {ex.Message}");
            targetDirectory = Path.Combine(Path.GetTempPath(), "CrashReports");
            Directory.CreateDirectory(targetDirectory);
            Console.WriteLine($"Falling back to local directory: {targetDirectory}");
        }

        // Set the output directory and a deterministic filename for the crash report
        crashOptions.CrashReportDirectory = targetDirectory; // directory where the report will be written
        crashOptions.CrashReportFilename = $"CrashReport_{DateTime.Now:yyyyMMdd_HHmmss}.html"; // optional – makes the path predictable

        // Generate the crash report
        PdfException.GenerateCrashReport(crashOptions);

        // Verify that the report file was created at the expected location
        string reportPath = crashOptions.CrashReportPath; // full path returned by the API after generation
        bool reportExists = File.Exists(reportPath);

        Console.WriteLine($"Crash report generated at: {reportPath}");
        Console.WriteLine($"Report exists: {reportExists}");
    }
}
