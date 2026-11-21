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

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Set up a TextAbsorber to extract text while preserving line breaks
                TextAbsorber absorber = new TextAbsorber
                {
                    ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
                };

                // Apply the absorber to all pages
                pdfDoc.Pages.Accept(absorber);
                string extractedText = absorber.Text;

                // Write the extracted text to a UTF‑8 encoded file (without BOM) preserving line breaks
                File.WriteAllText(outputTxt, extractedText, new System.Text.UTF8Encoding(false));

                Console.WriteLine($"Text extracted to '{outputTxt}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}