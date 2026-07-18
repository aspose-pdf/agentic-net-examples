using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, Page, Rectangle

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 12 pages (1‑based indexing)
            if (doc.Pages.Count < 12)
            {
                Console.Error.WriteLine("The document must contain at least 12 pages.");
                return;
            }

            // Retrieve the MediaBox rectangle from page 8
            Aspose.Pdf.Rectangle sourceMediaBox = doc.Pages[8].MediaBox;

            // Apply the same MediaBox to page 12
            doc.Pages[12].MediaBox = sourceMediaBox;

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}