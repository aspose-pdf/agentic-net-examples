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
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Set up a TextAbsorber with Pure formatting to keep line breaks
            TextAbsorber absorber = new TextAbsorber
            {
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };

            // Extract text from all pages
            pdfDoc.Pages.Accept(absorber);
            string extractedText = absorber.Text ?? string.Empty;

            // Write the extracted text to a UTF‑8 encoded file, preserving line breaks
            File.WriteAllText(outputTxtPath, extractedText, Encoding.UTF8);
        }

        Console.WriteLine($"Text extracted to '{outputTxtPath}'.");
    }
}