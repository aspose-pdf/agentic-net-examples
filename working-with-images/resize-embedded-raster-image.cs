using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API (Document, Page, Image, etc.)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF containing the raster image
        const string outputPath = "output_resized.pdf"; // destination PDF after resizing

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Iterate through all paragraph objects on the page
                // Image objects are stored as paragraphs of type Aspose.Pdf.Image
                for (int i = 1; i <= page.Paragraphs.Count; i++)
                {
                    if (page.Paragraphs[i] is Image img)
                    {
                        // Set the desired dimensions (values are in points; 1 point = 1/72 inch)
                        // Adjust these numbers to the required size
                        img.FixWidth  = 200; // new width
                        img.FixHeight = 150; // new height
                    }
                }
            }

            // Save the modified document (Document.Save(string) always writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}