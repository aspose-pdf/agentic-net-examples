using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document; using ensures proper disposal.
        using (Document doc = new Document(inputPath))
        {
            // Pages collection is 1‑based.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Rotate each page by 180 degrees.
                doc.Pages[i].Rotate = Rotation.on180;
            }

            // Save the rotated document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"All pages rotated 180° and saved to '{outputPath}'.");
    }
}