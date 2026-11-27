using System;
using System.IO;
using System.Text;
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
            // Create a TextAbsorber to extract text
            var absorber = new TextAbsorber();
            // Configure extraction to use Pure formatting mode
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Process only page 1 (Aspose.Pdf uses 1‑based page indexing)
            doc.Pages[1].Accept(absorber);

            // Retrieve the extracted text
            string extractedText = absorber.Text;

            // Save the text to a plain .txt file using UTF‑8 encoding
            File.WriteAllText(outputTxt, extractedText, Encoding.UTF8);
        }

        Console.WriteLine($"Text from page 1 saved to '{outputTxt}'.");
    }
}
