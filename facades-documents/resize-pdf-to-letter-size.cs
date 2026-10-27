using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_letter.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF using the file‑path overload of Document
        var doc = new Document(inputPath);

        // Letter size in points: 8.5" x 11" => 612 x 792 points
        const float letterWidth = 612f;
        const float letterHeight = 792f;

        // Resize every page to Letter dimensions
        foreach (Page page in doc.Pages)
        {
            page.SetPageSize(letterWidth, letterHeight);
        }

        // Save the resized document
        doc.Save(outputPath);

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}
