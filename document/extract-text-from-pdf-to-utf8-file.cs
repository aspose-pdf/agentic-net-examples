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

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Use TextAbsorber to extract all text from the document
                TextAbsorber absorber = new TextAbsorber();
                // Optional: configure extraction options (e.g., pure text without formatting)
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

                // Apply the absorber to all pages
                pdfDoc.Pages.Accept(absorber);

                // Retrieve the extracted text
                string extractedText = absorber.Text ?? string.Empty;

                // Write the text to a UTF-8 encoded file
                File.WriteAllText(outputTxtPath, extractedText, Encoding.UTF8);
            }

            Console.WriteLine($"Text extracted to '{outputTxtPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}