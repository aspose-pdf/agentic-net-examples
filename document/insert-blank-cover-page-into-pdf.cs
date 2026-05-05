using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_cover.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Insert an empty page at the very beginning (position 1)
            doc.Pages.Insert(1);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with cover page: '{outputPath}'.");
    }
}