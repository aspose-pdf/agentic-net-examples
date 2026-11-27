using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputPath = "FullText.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Extract all text from the document
            TextAbsorber absorber = new TextAbsorber
            {
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };
            doc.Pages.Accept(absorber);
            string fullText = absorber.Text;

            // Save the extracted text to a file
            File.WriteAllText(outputPath, fullText);
        }

        Console.WriteLine($"Extracted text saved to '{outputPath}'.");
    }
}