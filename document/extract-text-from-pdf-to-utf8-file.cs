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

        // Load the PDF document with deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Use TextAbsorber to extract all text from the document
            TextAbsorber absorber = new TextAbsorber
            {
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };
            pdfDoc.Pages.Accept(absorber);
            string extractedText = absorber.Text ?? string.Empty;

            // Write the extracted text to a UTF‑8 encoded file
            File.WriteAllText(outputTxt, extractedText, Encoding.UTF8);
        }

        Console.WriteLine($"Text extracted to '{outputTxt}'.");
    }
}