using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, Page, Rectangle

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 12 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 12)
            {
                Console.Error.WriteLine("Document does not contain 12 pages.");
                return;
            }

            // Get the MediaBox rectangle from page 8
            Aspose.Pdf.Rectangle sourceMediaBox = doc.Pages[8].MediaBox;

            // Apply the same MediaBox to page 12
            doc.Pages[12].MediaBox = sourceMediaBox;

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"MediaBox copied from page 8 to page 12 and saved as '{outputPath}'.");
    }
}