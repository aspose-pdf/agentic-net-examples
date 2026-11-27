using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "output.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // TextAbsorber extracts text; configure if needed
            TextAbsorber absorber = new TextAbsorber
            {
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };

            // Pages are 1‑based; extract from pages 26 and 27
            pdfDoc.Pages[26].Accept(absorber);
            pdfDoc.Pages[27].Accept(absorber);

            // Combine extracted text (absorber accumulates text from both pages)
            string combinedText = absorber.Text ?? string.Empty;

            // Write the result to a text file
            File.WriteAllText(outputTxtPath, combinedText);
        }

        Console.WriteLine($"Text from pages 26‑27 saved to '{outputTxtPath}'.");
    }
}