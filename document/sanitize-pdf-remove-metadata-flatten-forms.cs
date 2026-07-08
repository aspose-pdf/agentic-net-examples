using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Sanitizer <input-pdf-path> [output-pdf-path]");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Determine output path
        string outputPath;
        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
        {
            outputPath = args[1];
        }
        else
        {
            string dir = Path.GetDirectoryName(inputPath);
            string name = Path.GetFileNameWithoutExtension(inputPath);
            outputPath = Path.Combine(dir, $"{name}_sanitized.pdf");
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPath))
            {
                // Remove standard metadata (author, title, etc.)
                pdfDoc.RemoveMetadata();

                // Remove PDF/A and PDF/UA compliance flags if present
                pdfDoc.RemovePdfaCompliance();
                pdfDoc.RemovePdfUaCompliance();

                // Flatten form fields (replace fields with their appearance)
                pdfDoc.Flatten();

                // Optimize resources: remove unused objects and merge duplicates
                pdfDoc.OptimizeResources();

                // Linearize the document for faster web viewing (optional)
                pdfDoc.Optimize();

                // Save the sanitized PDF
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"Sanitized PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during sanitization: {ex.Message}");
        }
    }
}