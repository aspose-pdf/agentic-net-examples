using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "output.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Verify that the document has at least one page (pages are 1‑based)
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF contains no pages.");
                return;
            }

            // Create a TextAbsorber to extract plain text
            TextAbsorber absorber = new TextAbsorber
            {
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };

            // Apply the absorber to the first page
            doc.Pages[1].Accept(absorber);

            // Retrieve the extracted text (empty string if null)
            string extractedText = absorber.Text ?? string.Empty;

            // Write the text to a .txt file
            File.WriteAllText(outputTxt, extractedText);
        }

        Console.WriteLine($"Extracted text from page 1 saved to '{outputTxt}'.");
    }
}