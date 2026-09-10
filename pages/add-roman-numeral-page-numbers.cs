using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // for NumberingStyle enum

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "output.pdf";         // PDF with Roman page numbers
        const int introPages    = 5;                    // number of introductory pages

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document – wrapped in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over the introductory pages (1‑based indexing)
            for (int i = 1; i <= introPages && i <= doc.Pages.Count; i++)
            {
                // Create a page number stamp
                PageNumberStamp stamp = new PageNumberStamp();

                // Use uppercase Roman numerals (I, II, III, …)
                stamp.NumberingStyle = NumberingStyle.NumeralsRomanUppercase;

                // Position the stamp at the bottom‑center of the page
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment   = VerticalAlignment.Bottom;
                stamp.BottomMargin        = 20;   // distance from the bottom edge

                // Add the stamp to the current page
                doc.Pages[i].AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Roman page numbers added to first {introPages} pages. Saved as '{outputPath}'.");
    }
}