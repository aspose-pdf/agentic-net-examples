using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for HorizontalAlignment / VerticalAlignment enums if needed

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
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a PageNumberStamp with default format "#"
                PageNumberStamp stamp = new PageNumberStamp();

                // Position the stamp at the bottom center of the page
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment   = VerticalAlignment.Bottom;
                stamp.BottomMargin        = 20; // distance from the bottom edge

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF (Document.Save writes PDF regardless of extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Footer with page numbers added: {outputPath}");
    }
}