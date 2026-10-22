using System;
using System.IO;
using System.Text;
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
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Use TextAbsorber for text extraction (see text-extraction-use-textabsorber-not-page-extracttext rule)
            TextAbsorber absorber = new TextAbsorber
            {
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };
            pdfDoc.Pages.Accept(absorber);
            string allText = absorber.Text ?? string.Empty;

            // Write concatenated text to a UTF-8 encoded file
            File.WriteAllText(outputTxtPath, allText, Encoding.UTF8);
        }

        Console.WriteLine($"Text extracted to '{outputTxtPath}'.");
    }
}