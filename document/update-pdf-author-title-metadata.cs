using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF, modify metadata, and save
        using (Document doc = new Document(inputPath))
        {
            // Read existing metadata (optional, for demonstration)
            string currentAuthor = doc.Info.Author;
            string currentTitle  = doc.Info.Title;
            Console.WriteLine($"Current Author: {currentAuthor}");
            Console.WriteLine($"Current Title : {currentTitle}");

            // Modify metadata fields
            doc.Info.Author = "New Author Name";
            doc.Info.Title  = "New Document Title";

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}