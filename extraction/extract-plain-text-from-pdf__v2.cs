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

        // Load the PDF document with deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create a TextAbsorber to extract text from the whole document
            TextAbsorber absorber = new TextAbsorber
            {
                // Extract plain text without formatting tags
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };

            // Apply the absorber to all pages (pages are 1‑based internally)
            pdfDoc.Pages.Accept(absorber);

            // Concatenate the extracted text
            string allText = absorber.Text ?? string.Empty;

            // Save the concatenated text to a .txt file
            File.WriteAllText(outputTxt, allText);
        }

        Console.WriteLine($"Extracted text saved to '{outputTxt}'.");
    }
}