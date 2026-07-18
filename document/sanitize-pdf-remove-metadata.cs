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
                // Remove metadata (author, title, hidden info, etc.).
                doc.RemoveMetadata();

                // Remove PDF/A compliance if present.
                doc.RemovePdfaCompliance();

                // Remove PDF/UA compliance if present.
                doc.RemovePdfUaCompliance();

                // Flatten form fields and annotations to static content.
                doc.Flatten();

                // Optimize resources (remove unused objects, merge duplicates).
                doc.OptimizeResources();

                // Save the sanitized PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Sanitized PDF saved to: {outputPath}");
        }
        // Aspose.Pdf throws PdfException for PDF‑related errors.
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"Sanitization failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
