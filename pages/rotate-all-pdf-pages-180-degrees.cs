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

        // Load the PDF document; using ensures deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; rotate each page 180 degrees
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                doc.Pages[i].Rotate = Rotation.on180;
            }

            // Save the rotated document
            doc.Save(outputPath);
        }

        Console.WriteLine($"All pages rotated 180° and saved to '{outputPath}'.");
    }
}