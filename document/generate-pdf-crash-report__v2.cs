using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class CrashReportGenerator
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string reportPath = "crash_report.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Example operation: add a simple text fragment to the first page
                // (Replace with any complex PDF processing as needed)
                if (doc.Pages.Count > 0)
                {
                    var page = doc.Pages[1];
                    TextFragment text = new TextFragment("Processing successful.");
                    page.Paragraphs.Add(text);
                }

                // Save the modified document
                doc.Save(outputPdf);
                Console.WriteLine($"Document processed and saved to '{outputPdf}'.");
            }
        }
        catch (Exception ex)
        {
            // Build a custom stack trace message
            StringWriter customTrace = new StringWriter();
            customTrace.WriteLine("=== Crash Report ===");
            customTrace.WriteLine($"Timestamp: {DateTime.UtcNow:O}");
            customTrace.WriteLine($"Input File: {inputPdf}");
            customTrace.WriteLine($"Output File: {outputPdf}");
            customTrace.WriteLine($"Exception Type: {ex.GetType().FullName}");
            customTrace.WriteLine($"Message: {ex.Message}");
            customTrace.WriteLine("=== Original Stack Trace ===");
            customTrace.WriteLine(ex.StackTrace);
            customTrace.WriteLine("=== End of Report ===");

            // Write the report to a file
            try
            {
                File.WriteAllText(reportPath, customTrace.ToString());
                Console.Error.WriteLine($"An error occurred. Crash report written to '{reportPath}'.");
            }
            catch (Exception writeEx)
            {
                // If writing the report fails, output both errors to console
                Console.Error.WriteLine("Failed to write crash report:");
                Console.Error.WriteLine(writeEx);
                Console.Error.WriteLine("Original exception:");
                Console.Error.WriteLine(ex);
            }
        }
    }
}