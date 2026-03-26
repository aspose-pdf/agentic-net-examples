using System;
using System.IO;
using Aspose.Pdf;

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

        using (Document doc = new Document(inputPath))
        {
            // Read existing metadata
            string originalAuthor = doc.Info.Author;
            string originalTitle = doc.Info.Title;
            Console.WriteLine($"Original Author: {originalAuthor}");
            Console.WriteLine($"Original Title: {originalTitle}");

            // Modify metadata
            doc.Info.Author = "New Author Name";
            doc.Info.Title = "New Document Title";

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}