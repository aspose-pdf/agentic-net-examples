using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputTxtPath = "page1.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Prepare a TextAbsorber to extract text
            TextAbsorber absorber = new TextAbsorber
            {
                // Use pure text formatting (no layout information)
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };

            // Page indexing in Aspose.Pdf is 1‑based; extract from the first page
            doc.Pages[1].Accept(absorber);
            string extractedText = absorber.Text ?? string.Empty;

            // Write the extracted text to a UTF‑8 encoded file
            File.WriteAllText(outputTxtPath, extractedText, System.Text.Encoding.UTF8);
        }

        Console.WriteLine($"First page text saved to '{outputTxtPath}'.");
    }
}