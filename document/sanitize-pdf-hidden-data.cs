using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Sanitization; // For SanitizationException (if needed)

class Program
{
    static void Main(string[] args)
    {
        // Expect a single argument: the path to the input PDF.
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

        // Build the output path by appending "_sanitized" before the extension.
        string directory = Path.GetDirectoryName(inputPath);
        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
        string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_sanitized.pdf");

        try
        {
            // Load the PDF document (lifecycle rule: use using for disposal).
            using (Document doc = new Document(inputPath))
            {
                // Remove metadata (title, author, etc.).
                doc.RemoveMetadata();

                // Remove PDF/A compliance information if present.
                doc.RemovePdfaCompliance();

                // Remove PDF/UA compliance information if present.
                doc.RemovePdfUaCompliance();

                // Optimize resources: removes unused objects and merges duplicates.
                doc.OptimizeResources();

                // Save the sanitized PDF (lifecycle rule: save inside using block).
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