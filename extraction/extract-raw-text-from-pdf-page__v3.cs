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
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Verify that page 17 exists (Aspose.Pdf uses 1‑based indexing)
                if (doc.Pages.Count < 17)
                {
                    Console.Error.WriteLine("The document contains fewer than 17 pages.");
                    return;
                }

                // Get page 17
                Page page = doc.Pages[17];

                // Configure extraction to Raw mode
                TextExtractionOptions options = new TextExtractionOptions(
                    TextExtractionOptions.TextFormattingMode.Raw);

                // Use TextAbsorber to extract the raw content
                TextAbsorber absorber = new TextAbsorber(options);
                absorber.Visit(page);
                string rawContent = absorber.Text;

                // Write the extracted raw content to a text file
                File.WriteAllText(outputPath, rawContent ?? string.Empty);
                Console.WriteLine($"Raw content extracted to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
