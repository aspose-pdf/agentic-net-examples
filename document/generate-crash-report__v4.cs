using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "nonexistent.pdf";
        const string crashReportDir = "CrashReports";

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Perform a simple operation to trigger loading
                int pageCount = doc.Pages.Count;
                Console.WriteLine("Page count: " + pageCount);
            }
        }
        catch (PdfException pdfEx)
        {
            // Ensure the crash report directory exists
            if (!Directory.Exists(crashReportDir))
            {
                Directory.CreateDirectory(crashReportDir);
            }

            CrashReportOptions options = new CrashReportOptions(pdfEx);
            options.CrashReportDirectory = crashReportDir;
            options.CustomMessage = "Failed to load PDF. Input path: " + inputPath;
            PdfException.GenerateCrashReport(options);
            Console.WriteLine("Crash report generated at: " + options.CrashReportPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Unexpected error: " + ex.Message);
        }
    }
}