using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfManager
{
    static void Main()
    {
        // Input and output file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output_copy.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document using the recommended lifecycle pattern (using block)
        using (Document doc = new Document(inputPath))
        {
            // Basic information
            Console.WriteLine($"Pages: {doc.Pages.Count}");
            Console.WriteLine($"Title: {doc.Info.Title}");
            Console.WriteLine($"Author: {doc.Info.Author}");

            // Extract all text from the document using TextAbsorber (correct API)
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages.Accept(absorber);
            string extractedText = absorber.Text;
            Console.WriteLine($"Extracted text length: {extractedText.Length}");

            // Save a copy of the document (PDF format) using the standard Save method
            doc.Save(outputPath);
            Console.WriteLine($"Document saved to: {outputPath}");
        }
    }
}