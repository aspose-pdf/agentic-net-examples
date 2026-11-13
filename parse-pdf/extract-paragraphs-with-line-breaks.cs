using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TextAbsorber, TextExtractionOptions, etc.

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "extracted_paragraphs.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure TextAbsorber to preserve physical line breaks.
            TextAbsorber absorber = new TextAbsorber();
            // Use the constructor that accepts a TextFormattingMode enum value.
            absorber.ExtractionOptions = new TextExtractionOptions(
                TextExtractionOptions.TextFormattingMode.Pure);

            // Extract text from all pages.
            pdfDoc.Pages.Accept(absorber);
            string extractedText = absorber.Text; // Contains line breaks as in the PDF.

            // Write the extracted text to a .txt file.
            File.WriteAllText(outputTxtPath, extractedText);
        }

        Console.WriteLine($"Paragraphs extracted and saved to '{outputTxtPath}'.");
    }
}
