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

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure text extraction to preserve line breaks (Pure mode)
                TextAbsorber absorber = new TextAbsorber
                {
                    ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
                };

                // Extract text from all pages
                pdfDoc.Pages.Accept(absorber);

                // Write the extracted text to a UTF‑8 encoded file, preserving line breaks
                File.WriteAllText(outputTxtPath, absorber.Text, Encoding.UTF8);
            }

            Console.WriteLine($"Text extracted to '{outputTxtPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}