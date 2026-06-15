using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF file (must exist)
        const string inputPdfPath = "input.pdf";
        // Output text file
        const string outputTxtPath = "extracted_text.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a TextAbsorber to extract visible text.
            // Use Pure formatting mode to get readable text without hidden OCR layers.
            TextAbsorber absorber = new TextAbsorber
            {
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };

            // Apply the absorber to all pages of the document
            pdfDoc.Pages.Accept(absorber);

            // Retrieve the extracted text
            string extractedText = absorber.Text;

            // Write the text to the output file
            File.WriteAllText(outputTxtPath, extractedText);
        }

        Console.WriteLine($"Text extracted to '{outputTxtPath}'.");
    }
}