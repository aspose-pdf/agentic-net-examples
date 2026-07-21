using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create a page‑number stamp. The default format "#" will be replaced
                // with the actual page number when the stamp is applied.
                PageNumberStamp pageNumberStamp = new PageNumberStamp();

                // Position the stamp at the bottom centre of the page
                pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
                pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
                pageNumberStamp.BottomMargin        = 10; // distance from the bottom edge

                // Add the stamp to the current page
                page.AddStamp(pageNumberStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Footer with page numbers added. Saved to '{outputPath}'.");
    }
}