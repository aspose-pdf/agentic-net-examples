using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "sanitized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Ensure signature fields are sanitized (default is true)
                doc.EnableSignatureSanitization = true;

                // Remove all document metadata (author, title, etc.)
                doc.RemoveMetadata();

                // Remove PDF/A and PDF/UA compliance flags
                doc.RemovePdfaCompliance();
                doc.RemovePdfUaCompliance();

                // Flatten form fields so only their visual appearance remains
                doc.Flatten();

                // Optimize resources and linearize the document for faster web access
                doc.OptimizeResources();
                doc.Optimize();

                // Save the sanitized PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during sanitization: {ex.Message}");
        }
    }
}