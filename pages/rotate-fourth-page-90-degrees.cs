using System;
using System.IO;
using Aspose.Pdf; // Document, Page, Rotation

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_page4.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least four pages (1‑based indexing)
            if (doc.Pages.Count >= 4)
            {
                // Rotate the fourth page 90 degrees clockwise
                doc.Pages[4].Rotate = Rotation.on90;
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