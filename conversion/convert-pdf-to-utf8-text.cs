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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a TextAbsorber to extract text; set extraction options to preserve line breaks
            TextAbsorber absorber = new TextAbsorber
            {
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };

            // Apply the absorber to all pages
            pdfDocument.Pages.Accept(absorber);

            // Retrieve the extracted text (line breaks are preserved by the Pure mode)
            string extractedText = absorber.Text;

            // Write the text to a file using UTF‑8 encoding
            File.WriteAllText(outputTxtPath, extractedText, Encoding.UTF8);
        }

        Console.WriteLine($"Text extraction completed. Output saved to '{outputTxtPath}'.");
    }
}