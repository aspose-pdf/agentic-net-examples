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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least four pages (1‑based indexing)
            if (doc.Pages.Count >= 4)
            {
                // Rotate page 4 by 90 degrees clockwise
                doc.Pages[4].Rotate = Aspose.Pdf.Rotation.on90;
            }
            else
            {
                Console.Error.WriteLine("The document contains fewer than 4 pages.");
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}