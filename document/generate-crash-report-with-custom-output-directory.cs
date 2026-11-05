using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths
        const string inputPdf = "sample.pdf";               // existing PDF
        const string crashDir = "CrashReports";             // directory for crash reports

        // Ensure the crash report directory exists
        Directory.CreateDirectory(crashDir);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Intentionally cause an exception (invalid page index) to trigger a crash report
                // Aspose.Pdf uses 1‑based indexing; page 0 will throw IndexOutOfRangeException
                Page invalidPage = doc.Pages[0];
                Console.WriteLine(invalidPage);
            }
        }
        catch (Exception ex)
        {
            // Generate a crash report in the specified directory
            var crashOptions = new CrashReportOptions(ex)
            {
                CrashReportDirectory = crashDir,
                CrashReportFilename = "crash_report.html",
                CustomMessage = "Unexpected error during PDF processing."
            };
            PdfException.GenerateCrashReport(crashOptions);

            Console.WriteLine($"Caught exception: {ex.Message}");
        }

        // Verify that at least one crash report file was created in the output directory
        string[] reportFiles = Directory.GetFiles(crashDir);
        if (reportFiles.Length > 0)
        {
            Console.WriteLine($"Crash report generated successfully. Files:");
            foreach (string file in reportFiles)
                Console.WriteLine($"  {Path.GetFileName(file)}");
        }
        else
        {
            Console.WriteLine("No crash report files were found. Verification failed.");
        }
    }
}