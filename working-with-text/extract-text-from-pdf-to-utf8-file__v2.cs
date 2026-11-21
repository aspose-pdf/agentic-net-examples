using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputTxtPath = "output.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Use TextAbsorber to extract all text from the document
            TextAbsorber absorber = new TextAbsorber
            {
                // Extract plain text without formatting tags
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };
            pdfDoc.Pages.Accept(absorber);
            string extractedText = absorber.Text ?? string.Empty;

            // Write the concatenated text to a UTF-8 encoded file
            File.WriteAllText(outputTxtPath, extractedText, Encoding.UTF8);
        }

        Console.WriteLine($"Text extracted to '{outputTxtPath}'.");
    }
}