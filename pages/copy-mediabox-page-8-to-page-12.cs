using System;
using System.IO;
using Aspose.Pdf;               // Core API
// Fully qualify Rectangle to avoid ambiguity with System.Drawing
// Aspose.Pdf.Rectangle is the correct type for page boxes.

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 12 pages
            if (doc.Pages.Count < 12)
            {
                Console.Error.WriteLine("Document does not contain 12 pages.");
                return;
            }

            // Get the MediaBox rectangle from page 8 (1‑based indexing)
            Aspose.Pdf.Rectangle mediaBox = doc.Pages[8].MediaBox;

            // Apply the same MediaBox to page 12
            doc.Pages[12].MediaBox = mediaBox;

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"MediaBox copied from page 8 to page 12 and saved as '{outputPath}'.");
    }
}