using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string newAuthor  = "John Doe";
        const string newTitle   = "Updated Document Title";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Read existing metadata
            string oldAuthor = doc.Info.Author;
            string oldTitle  = doc.Info.Title;

            Console.WriteLine($"Original Author: {oldAuthor}");
            Console.WriteLine($"Original Title : {oldTitle}");

            // Modify metadata fields
            doc.Info.Author = newAuthor;
            doc.Info.Title  = newTitle;

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}