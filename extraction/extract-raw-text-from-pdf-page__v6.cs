using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.txt";
        const int pageNumber = 34; // 1‑based index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the requested page exists
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} out of range (1‑{doc.Pages.Count}).");
                return;
            }

            // Configure extraction to Raw mode (preserves the original PDF text stream)
            var extractionOpts = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Raw);

            // Use TextAbsorber (TextDevice does not exist in the core API)
            var absorber = new TextAbsorber
            {
                ExtractionOptions = extractionOpts
            };

            // Process only the requested page
            doc.Pages[pageNumber].Accept(absorber);

            // Write the raw page content to the output text file (UTF‑8 encoding)
            File.WriteAllText(outputPath, absorber.Text, Encoding.UTF8);
        }

        Console.WriteLine($"Raw content of page {pageNumber} saved to '{outputPath}'.");
    }
}
