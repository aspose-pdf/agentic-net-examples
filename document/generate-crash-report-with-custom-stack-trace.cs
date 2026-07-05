using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (load rule)
            using (Document doc = new Document(inputPath))
            {
                // Example of a complex operation that may fail:
                // Attempt to access a page beyond the last page to trigger an exception
                Page page = doc.Pages[doc.Pages.Count + 1];

                // Save the PDF document (save rule)
                doc.Save(outputPath);
            }
        }
        catch (Exception ex)
        {
            // Create crash report options based on the caught exception
            CrashReportOptions options = new CrashReportOptions(ex)
            {
                // Include a custom message with the stack trace for debugging
                CustomMessage = $"Custom stack trace for debugging:\n{ex.StackTrace}",
                // Set the directory where the crash report will be written
                CrashReportDirectory = Directory.GetCurrentDirectory()
            };

            // Generate the crash report (static method)
            PdfException.GenerateCrashReport(options);

            Console.Error.WriteLine($"An error occurred: {ex.Message}");
            Console.Error.WriteLine("Crash report generated.");
        }
    }
}