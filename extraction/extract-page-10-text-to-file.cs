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
                // Ensure the document has at least 10 pages (1‑based indexing)
                if (doc.Pages.Count < 10)
                {
                    Console.Error.WriteLine("The document contains fewer than 10 pages.");
                    return;
                }

                // Use TextAbsorber (core API) to extract all text from page 10
                TextAbsorber absorber = new TextAbsorber();
                absorber.Visit(doc.Pages[10]);

                // The extracted text for the whole page is available via the Text property
                string extractedText = absorber.Text ?? string.Empty;

                // Write the extracted content to the output text file
                File.WriteAllText(outputPath, extractedText);
            }

            Console.WriteLine($"Paragraph content from page 10 saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
