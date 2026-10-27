using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Desired network share for crash reports (UNC path)
        string networkShare = @"\\myserver\share\CrashReports";
        string crashReportDir;

        // Try to create the network directory; if it fails, fall back to a local temp folder
        try
        {
            Directory.CreateDirectory(networkShare);
            crashReportDir = networkShare;
        }
        catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is NotSupportedException)
        {
            // Network path unavailable – use a local temporary directory instead
            crashReportDir = Path.Combine(Path.GetTempPath(), "AsposeCrashReports");
            Directory.CreateDirectory(crashReportDir);
            Console.WriteLine($"Network share unavailable. Crash reports will be written to local folder: {crashReportDir}");
        }

        // Force an exception to trigger a crash report
        try
        {
            // Attempt to load a non‑existent PDF file
            using (Document doc = new Document("nonexistent.pdf"))
            {
                // No further action needed – the constructor will throw
            }
        }
        catch (Exception ex)
        {
            // Configure crash‑report options with the resolved directory
            var crashOptions = new CrashReportOptions(ex)
            {
                CrashReportDirectory = crashReportDir,
                CrashReportFilename = "crash_report.html",
                CustomMessage = "Unexpected error during PDF processing."
            };

            // Generate the crash report manually
            PdfException.GenerateCrashReport(crashOptions);
        }

        // Verify that a crash report file was written to the chosen location
        bool reportExists = false;
        if (Directory.Exists(crashReportDir))
        {
            foreach (string filePath in Directory.GetFiles(crashReportDir, "*.html"))
            {
                reportExists = true;
                Console.WriteLine($"Crash report found: {Path.GetFileName(filePath)}");
                break; // One report is sufficient for verification
            }
        }

        Console.WriteLine(reportExists
            ? "Crash report successfully written to the target folder."
            : "No crash report was found in the target folder.");
    }
}
