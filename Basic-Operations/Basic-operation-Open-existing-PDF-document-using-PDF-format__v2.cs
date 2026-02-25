using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_copy.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the existing PDF document using a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Basic information about the opened PDF
            Console.WriteLine($"Pages: {doc.Pages.Count}");
            Console.WriteLine($"Title:  {doc.Info.Title}");
            Console.WriteLine($"Author: {doc.Info.Author}");
            Console.WriteLine($"Subject:{doc.Info.Subject}");

            // Save a copy of the document (plain PDF save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document opened and saved to '{outputPath}'.");
    }
}