using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Verify that page 20 exists (Aspose.Pdf uses 1‑based indexing)
                if (doc.Pages.Count < 20)
                {
                    Console.Error.WriteLine("The document contains fewer than 20 pages.");
                    return;
                }

                Page page = doc.Pages[20];

                // Configure extraction options to retrieve raw content
                TextExtractionOptions extractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Raw);

                // Use TextAbsorber (core API) instead of the removed TextDevice class
                TextAbsorber absorber = new TextAbsorber
                {
                    ExtractionOptions = extractionOptions
                };

                // Process the specific page
                absorber.Visit(page);

                // Save the raw text to a file
                File.WriteAllText(outputPath, absorber.Text);
            }

            Console.WriteLine($"Raw content of page 20 saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
