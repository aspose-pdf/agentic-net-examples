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
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a TextAbsorber to extract all text from the document
            TextAbsorber absorber = new TextAbsorber();

            // Configure extraction options if needed (e.g., pure text formatting)
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Apply the absorber to all pages
            pdfDoc.Pages.Accept(absorber);

            // The extracted text contains line breaks; treat each line as a paragraph
            string extractedText = absorber.Text ?? string.Empty;

            // Write the extracted paragraphs to the output text file
            File.WriteAllText(outputTxtPath, extractedText);
        }

        Console.WriteLine($"Paragraph text extracted to '{outputTxtPath}'.");
    }
}