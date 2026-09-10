using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "odd_page_numbers.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Apply numbering only to odd pages
                if (i % 2 == 1)
                {
                    // Create a page number stamp; default format "#" will be replaced by the page number
                    PageNumberStamp stamp = new PageNumberStamp();

                    // Optional visual settings
                    stamp.HorizontalAlignment = HorizontalAlignment.Center;
                    stamp.VerticalAlignment   = VerticalAlignment.Bottom;
                    stamp.BottomMargin        = 20; // distance from the bottom edge

                    // Add the stamp to the current page
                    stamp.Put(doc.Pages[i]);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with page numbers on odd pages: '{outputPath}'.");
    }
}