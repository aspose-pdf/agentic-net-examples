using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input file exists before processing
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Example operation: extract all text from the document
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages.Accept(absorber);
                string extractedText = absorber.Text;

                // (Optional) Use the extracted text for further processing here

                // Save the (potentially modified) document; no SaveOptions needed for PDF output
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processing completed successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Build a detailed crash report with a custom stack trace
            string crashReport = BuildCustomStackTrace(ex);

            // Write the report to a file for later analysis
            const string reportPath = "crash_report.txt";
            File.WriteAllText(reportPath, crashReport);

            Console.Error.WriteLine($"An error occurred. See '{reportPath}' for details.");
        }
    }

    // Constructs a custom stack trace including timestamp, context, and inner exceptions
    static string BuildCustomStackTrace(Exception ex)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"Timestamp: {DateTime.UtcNow:O}");
        sb.AppendLine($"Exception Type: {ex.GetType().FullName}");
        sb.AppendLine($"Message: {ex.Message}");
        sb.AppendLine("Custom Context: PDF processing operation failed.");
        sb.AppendLine("Original StackTrace:");
        sb.AppendLine(ex.StackTrace ?? "No stack trace available.");

        // Include any inner exceptions recursively
        Exception inner = ex.InnerException;
        while (inner != null)
        {
            sb.AppendLine("--- Inner Exception ---");
            sb.AppendLine($"Type: {inner.GetType().FullName}");
            sb.AppendLine($"Message: {inner.Message}");
            sb.AppendLine(inner.StackTrace ?? "No stack trace available.");
            inner = inner.InnerException;
        }

        return sb.ToString();
    }
}