using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "nobookmarks.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Delete all bookmarks (outlines). The Delete method expects a bookmark title (string),
            // so to remove everything we clear the collection.
            doc.Outlines.Clear();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All bookmarks removed. Saved as '{outputPath}'.");
    }
}
