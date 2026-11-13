using System;
using System.IO;
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
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Use TextAbsorber to extract text from all pages
            TextAbsorber absorber = new TextAbsorber();
            // Optional: configure extraction options (e.g., pure text without formatting)
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Apply the absorber to the document's pages
            pdfDoc.Pages.Accept(absorber);

            // Concatenated text from the entire document
            string allText = absorber.Text ?? string.Empty;

            // Write the extracted text to a .txt file
            File.WriteAllText(outputTxtPath, allText);
        }

        Console.WriteLine($"Text extracted and saved to '{outputTxtPath}'.");
    }
}