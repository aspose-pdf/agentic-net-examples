using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over pages using 1‑based indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Apply page number stamp only on odd pages
                if (i % 2 == 1)
                {
                    // Create a PageNumberStamp; default format "#" will be replaced by the page number
                    PageNumberStamp stamp = new PageNumberStamp();

                    // Optional: position the stamp (e.g., bottom‑center)
                    stamp.HorizontalAlignment = HorizontalAlignment.Center;
                    stamp.VerticalAlignment   = VerticalAlignment.Bottom;
                    stamp.BottomMargin        = 20; // distance from bottom edge

                    // Add the stamp to the current page
                    doc.Pages[i].AddStamp(stamp);
                }
            }

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Odd‑page numbers added. Output saved to '{outputPath}'.");
    }
}