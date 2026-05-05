using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace includes Document, PdfException, CrashReportOptions

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document using a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Example of a complex operation that may throw an exception:
                // Attempt to access a page using an invalid (0‑based) index to trigger an error.
                // Aspose.Pdf uses 1‑based indexing; this will raise an IndexOutOfRangeException.
                Page invalidPage = doc.Pages[0];

                // Perform additional processing here...
                // (omitted – focus is on crash‑report generation)
            }
        }
        catch (Exception ex)
        {
            // Prepare a custom crash report with additional diagnostic information
            CrashReportOptions reportOptions = new CrashReportOptions(ex)
            {
                // Directory where the HTML crash report will be written
                CrashReportDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CrashReports"),

                // Optional custom message – can include variable values, stack trace snippets, etc.
                CustomMessage = $"Custom stack trace for debugging:\n{ex.StackTrace}"
            };

            // Ensure the output directory exists
            Directory.CreateDirectory(reportOptions.CrashReportDirectory);

            // Generate the crash report (HTML file) based on the caught exception
            PdfException.GenerateCrashReport(reportOptions);

            Console.Error.WriteLine($"Crash report generated at: {reportOptions.CrashReportPath}");
        }
    }
}