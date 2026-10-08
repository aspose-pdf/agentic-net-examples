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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify the document has at least four pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The document contains fewer than 4 pages.");
                return;
            }

            // Rotate page 4 by 90 degrees clockwise using the Rotate property
            Page page = doc.Pages[4];
            page.Rotate = Rotation.on90; // 90° clockwise

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 4 rotated and saved to '{outputPath}'.");
    }
}
