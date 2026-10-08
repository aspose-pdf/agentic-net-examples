using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define the directory where crash reports will be written
        string crashReportDir = Path.Combine(Directory.GetCurrentDirectory(), "CrashReports");
        Directory.CreateDirectory(crashReportDir);

        // Intentionally cause an Aspose.Pdf error to generate a crash report
        try
        {
            // Loading a non‑existent file throws an exception and triggers the crash report
            using (Document doc = new Document("nonexistent.pdf"))
            {
                // No further actions needed
            }
        }
        catch (Exception ex)
        {
            // Create CrashReportOptions with the caught exception and configure the output location
            var crashOptions = new CrashReportOptions(ex)
            {
                CrashReportDirectory = crashReportDir,
                CrashReportFilename = "crash_report.html",
                CustomMessage = "Unexpected error during PDF processing."
            };
            // Generate the crash report manually
            PdfException.GenerateCrashReport(crashOptions);
        }

        // Verify that a crash report file was saved in the specified directory
        bool reportExists = Directory.GetFiles(crashReportDir, "*.html", SearchOption.TopDirectoryOnly).Length > 0;
        Console.WriteLine(reportExists
            ? $"Crash report successfully saved to '{crashReportDir}'."
            : $"No crash report found in '{crashReportDir}'.");
    }
}
