using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "sanitized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal (rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Remove standard metadata (author, title, etc.)
            doc.RemoveMetadata();

            // Remove PDF/A compliance information if present
            doc.RemovePdfaCompliance();

            // Remove PDF/UA (accessibility) compliance information if present
            doc.RemovePdfUaCompliance();

            // Flatten transparent content to eliminate hidden layers that rely on transparency
            doc.FlattenTransparency();

            // Optimize resources: remove unused objects and merge duplicates
            doc.OptimizeResources();

            // Save the sanitized PDF (Document.Save(string) always writes PDF regardless of extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}