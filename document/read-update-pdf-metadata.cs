using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Read existing metadata (optional, just for demonstration)
            Console.WriteLine($"Original Author: {doc.Info.Author}");
            Console.WriteLine($"Original Title : {doc.Info.Title}");

            // Modify the Author and Title fields
            doc.Info.Author = "New Author Name";
            doc.Info.Title  = "New Document Title";

            // Save the changes back to a PDF file
            doc.Save(outputPath);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}