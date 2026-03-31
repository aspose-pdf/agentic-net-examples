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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;
            // Aspose.Pdf pages are 1‑based, so start at 3 and step by 3
            for (int pageIndex = 3; pageIndex <= pageCount; pageIndex += 3)
            {
                // Rotate the page by 270 degrees (landscape orientation)
                doc.Pages[pageIndex].Rotate = Rotation.on270;
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated every third page and saved to '{outputPath}'.");
    }
}
