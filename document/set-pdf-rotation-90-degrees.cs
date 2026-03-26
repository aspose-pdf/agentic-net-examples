using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Set rotation of each page to 90 degrees (portrait on landscape devices)
            foreach (Page page in doc.Pages)
            {
                page.Rotate = Rotation.on90; // Correct enum value
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with rotation applied: {outputPath}");
    }
}