using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string inputPath = "nonexistent.pdf";
        try
        {
            // Attempt to load a PDF file that does not exist to trigger a PdfException
            using (Document doc = new Document(inputPath))
            {
                // No further processing needed
            }
        }
        catch (PdfException pdfEx)
        {
            // Create crash report options based on the caught exception
            CrashReportOptions options = new CrashReportOptions(pdfEx);
            options.CustomMessage = "Error occurred while loading PDF.";
            // Generate the crash report HTML file
            PdfException.GenerateCrashReport(options);
            Console.WriteLine("Crash report generated at: " + options.CrashReportPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Unexpected error: " + ex.Message);
        }
    }
}