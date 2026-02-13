using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Simulate an operation that may throw an exception.
        try
        {
            // Replace this with real code that may fail.
            throw new InvalidOperationException("Simulated crash for report generation.");
        }
        catch (Exception ex)
        {
            try
            {
                // Prepare crash‑report options.
                string workingDir = Directory.GetCurrentDirectory();
                CrashReportOptions reportOptions = new CrashReportOptions(ex)
                {
                    CrashReportDirectory = workingDir,
                    CrashReportFilename = "CrashReport.html"
                };

                // Generate the HTML crash report.
                PdfException.GenerateCrashReport(reportOptions);

                // Full path to the generated HTML file.
                string htmlReportPath = Path.Combine(reportOptions.CrashReportDirectory, reportOptions.CrashReportFilename);

                // Verify that the HTML file was actually created.
                if (!File.Exists(htmlReportPath))
                {
                    throw new FileNotFoundException("Crash report HTML file not found.", htmlReportPath);
                }

                // Load the HTML file as a PDF document using HtmlLoadOptions.
                HtmlLoadOptions loadOptions = new HtmlLoadOptions();
                Document pdfDocument = new Document(htmlReportPath, loadOptions);

                // Define the output PDF file path.
                string pdfReportPath = Path.Combine(reportOptions.CrashReportDirectory, "CrashReport.pdf");

                // Save the document as PDF.
                pdfDocument.Save(pdfReportPath);

                Console.WriteLine($"Crash report successfully saved to: {pdfReportPath}");
            }
            catch (Exception innerEx)
            {
                // Log any errors that occur while generating the PDF report.
                Console.Error.WriteLine($"Failed to generate PDF crash report: {innerEx.Message}");
            }
        }
    }
}