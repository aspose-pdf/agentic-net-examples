using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_page4.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least four pages (1‑based indexing)
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The document contains fewer than 4 pages.");
                return;
            }

            // Rotate page 4 by 90 degrees clockwise using the Rotate property.
            doc.Pages[4].Rotate = Rotation.on90;

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 4 rotated and saved to '{outputPath}'.");
    }
}
