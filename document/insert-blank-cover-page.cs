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
            // Insert a blank page at the beginning (position 1)
            doc.Pages.Insert(1);
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank cover page inserted. Saved to '{outputPath}'.");
    }
}