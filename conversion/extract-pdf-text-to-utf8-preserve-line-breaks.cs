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

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Use TextAbsorber to extract all text, preserving line breaks
                TextAbsorber absorber = new TextAbsorber();
                // Optional: set extraction mode to preserve layout (line breaks)
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                pdfDoc.Pages.Accept(absorber);

                string extractedText = absorber.Text ?? string.Empty;

                // Write the extracted text to a UTF‑8 encoded file, preserving line breaks
                File.WriteAllText(outputTxtPath, extractedText, new UTF8Encoding(false));
                Console.WriteLine($"Text extracted to '{outputTxtPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}