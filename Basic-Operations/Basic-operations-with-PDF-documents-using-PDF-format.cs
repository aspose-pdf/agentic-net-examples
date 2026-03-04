using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Basic information
            Console.WriteLine($"Pages : {doc.Pages.Count}");
            Console.WriteLine($"Author: {doc.Info.Author}");
            Console.WriteLine($"Title : {doc.Info.Title}");

            // Extract all text from the document using TextAbsorber
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages.Accept(absorber);
            string extractedText = absorber.Text;
            Console.WriteLine($"Extracted text length: {extractedText.Length}");

            // Save a copy of the document (PDF format)
            doc.Save(outputPath);
            Console.WriteLine($"Document saved to '{outputPath}'.");
        }
    }
}