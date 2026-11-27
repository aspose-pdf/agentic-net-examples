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
        const int targetPageNumber = 14; // 1‑based index

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the requested page exists
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPageNumber} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Set up a TextAbsorber to extract formatted (pure) text from the specific page
            TextAbsorber absorber = new TextAbsorber
            {
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };

            // Apply the absorber only to the desired page
            doc.Pages[targetPageNumber].Accept(absorber);

            // Retrieve the extracted text
            string extractedText = absorber.Text ?? string.Empty;

            // Write the result to the output file
            try
            {
                File.WriteAllText(outputTxt, extractedText);
                Console.WriteLine($"Formatted text from page {targetPageNumber} written to '{outputTxt}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to write output file: {ex.Message}");
            }
        }
    }
}