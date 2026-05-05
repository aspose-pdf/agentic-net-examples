using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Sanitization; // for SanitizationException

class Program
{
    static void Main(string[] args)
    {
        // Expect a single argument: the path to the PDF to sanitize
        if (args.Length != 1)
        {
            Console.Error.WriteLine("Usage: Sanitizer <input-pdf-path>");
            return;
        }

        string inputPath = args[0];

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Build output path by appending "_sanitized" before the extension
        string directory = Path.GetDirectoryName(inputPath);
        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
        string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_sanitized.pdf");

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Remove metadata (author, title, custom properties, etc.)
                doc.RemoveMetadata();

                // Remove PDF/A compliance information if present
                doc.RemovePdfaCompliance();

                // Remove PDF/UA compliance information if present
                doc.RemovePdfUaCompliance();

                // Flatten form fields and annotations to plain content
                doc.Flatten();

                // Optimize resources (remove unused objects, merge duplicates)
                doc.OptimizeResources();

                // Save the sanitized PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Sanitized PDF saved to: {outputPath}");
        }
        catch (SanitizationException ex)
        {
            Console.Error.WriteLine($"Sanitization failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}