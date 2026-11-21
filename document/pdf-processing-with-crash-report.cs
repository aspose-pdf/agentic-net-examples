using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for TextAbsorber

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string logPath = "error.log";

        // Verify that the input file exists before processing
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
                // Example processing: output page count
                int pageCount = doc.Pages.Count;
                Console.WriteLine($"Pages: {pageCount}");

                // Example processing: extract all text using TextAbsorber
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages.Accept(absorber);
                Console.WriteLine($"Extracted text length: {absorber.Text?.Length ?? 0}");

                // Save the (potentially modified) document
                doc.Save(outputPath);
                Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
            }
        }
        // Capture Aspose.Pdf specific exceptions for more precise logging
        catch (PdfException pdfEx)
        {
            LogException(pdfEx, logPath);
        }
        // Capture any other unexpected exceptions
        catch (Exception ex)
        {
            LogException(ex, logPath);
        }
    }

    // Helper method that writes a detailed crash report to a log file
    static void LogException(Exception ex, string logFile)
    {
        try
        {
            // Append to the log file to preserve previous entries
            using (StreamWriter writer = new StreamWriter(logFile, true))
            {
                writer.WriteLine("=== Crash Report ===");
                writer.WriteLine($"Timestamp : {DateTime.UtcNow:O}");
                writer.WriteLine($"Message   : {ex.Message}");
                writer.WriteLine($"Source    : {ex.Source}");
                writer.WriteLine($"StackTrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    writer.WriteLine("--- Inner Exception ---");
                    writer.WriteLine($"Message   : {ex.InnerException.Message}");
                    writer.WriteLine($"StackTrace: {ex.InnerException.StackTrace}");
                }
                writer.WriteLine(); // blank line for readability
            }
            Console.Error.WriteLine($"An error occurred. Details written to '{logFile}'.");
        }
        catch
        {
            // If logging itself fails, fall back to console output
            Console.Error.WriteLine("Failed to write crash report to log file.");
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}