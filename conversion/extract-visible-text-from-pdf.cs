using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfToTextExtractor
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "output.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a TextAbsorber to extract visible text
            TextAbsorber absorber = new TextAbsorber();

            // Configure extraction to use the default (Pure) formatting mode,
            // which extracts only the visible text content.
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Apply the absorber to all pages of the document
            pdfDoc.Pages.Accept(absorber);

            // Retrieve the extracted text
            string extractedText = absorber.Text ?? string.Empty;

            // Write the text to a .txt file
            File.WriteAllText(outputTxtPath, extractedText);
        }

        Console.WriteLine($"Text extracted to '{outputTxtPath}'.");
    }
}