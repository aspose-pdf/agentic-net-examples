using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expect a single argument: the path to the input PDF file.
        if (args.Length != 1)
        {
            Console.Error.WriteLine("Usage: Sanitizer <input-pdf-path>");
            return;
        }

        string inputPath = args[0];

        // Verify that the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Build the output path by inserting "_sanitized" before the extension.
        string directory = Path.GetDirectoryName(inputPath) ?? "";
        string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
        string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_sanitized.pdf");

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPath))
            {
                // Remove metadata (author, title, custom properties, etc.).
                doc.RemoveMetadata();

                // Remove PDF/A and PDF/UA compliance markers if present.
                doc.RemovePdfaCompliance();
                doc.RemovePdfUaCompliance();

                // Optimize resources: drop unused objects and merge duplicates.
                doc.OptimizeResources();

                // Save the sanitized PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Sanitized PDF saved to: {outputPath}");
        }
        // Aspose.Pdf throws PdfException for PDF‑related errors.
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General error handling.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
