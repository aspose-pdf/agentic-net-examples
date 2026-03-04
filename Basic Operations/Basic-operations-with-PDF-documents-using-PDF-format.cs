using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Display basic document information
                Console.WriteLine($"Pages: {doc.Pages.Count}");
                Console.WriteLine($"Author: {doc.Info.Author}");
                Console.WriteLine($"Title: {doc.Info.Title}");

                // Extract all text from the document using TextAbsorber
                TextAbsorber absorber = new TextAbsorber
                {
                    ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
                };
                doc.Pages.Accept(absorber);
                Console.WriteLine($"Extracted text length: {absorber.Text?.Length ?? 0}");

                // Save a copy of the document in PDF format
                doc.Save(outputPath);
            }

            Console.WriteLine($"Document processed and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}