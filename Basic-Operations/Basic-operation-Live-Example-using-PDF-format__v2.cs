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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Basic information
                Console.WriteLine($"Pages : {doc.Pages.Count}");
                Console.WriteLine($"Author: {doc.Info.Author}");
                Console.WriteLine($"Title : {doc.Info.Title}");

                // Extract all text using TextAbsorber (the correct API for text extraction)
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages.Accept(absorber);
                Console.WriteLine($"Extracted text length: {absorber.Text.Length}");

                // Save a copy of the document (plain PDF save)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Document saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}