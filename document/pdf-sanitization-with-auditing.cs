using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Sanitization;

class PdfSanitizer
{
    /// <summary>
    /// Sanitizes a PDF document, logs start and end times, and saves the sanitized output.
    /// </summary>
    /// <param name="inputPath">Path to the source PDF.</param>
    /// <param name="outputPath">Path where the sanitized PDF will be saved.</param>
    /// <param name="logPath">Path to the validation log file.</param>
    public static void Sanitize(string inputPath, string outputPath, string logPath)
    {
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        DateTime startTime = DateTime.UtcNow;
        Console.WriteLine($"Sanitization started: {startTime:O}");

        try
        {
            // Load the PDF document (using rule: load with Document constructor)
            using (Document doc = new Document(inputPath))
            {
                // Enable signature field sanitization (property exists on Document)
                doc.EnableSignatureSanitization = true;

                // Perform validation which also acts as a sanitization step.
                // The Validate method writes a log file and returns a bool indicating success.
                bool validationResult = doc.Validate(logPath, PdfFormat.PDF_A_1B);
                if (!validationResult)
                {
                    Console.Error.WriteLine("Validation reported issues during sanitization.");
                }

                // Save the (potentially sanitized) document (using rule: save with Document.Save)
                doc.Save(outputPath);
            }
        }
        catch (SanitizationException ex)
        {
            // Specific exception for sanitization failures
            Console.Error.WriteLine($"Sanitization failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General error handling
            Console.Error.WriteLine($"Error: {ex.Message}");
        }

        DateTime endTime = DateTime.UtcNow;
        Console.WriteLine($"Sanitization ended: {endTime:O}");
        Console.WriteLine($"Duration: {(endTime - startTime).TotalSeconds:F2} seconds");
    }

    // Example usage
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "sanitized_output.pdf";
        const string logFile   = "sanitization_log.txt";

        Sanitize(inputPdf, outputPdf, logFile);
    }
}