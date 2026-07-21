using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for page.AddStamp extension
using Aspose.Pdf.Facades;   // not required but kept for completeness

class InsertRomanPageNumbers
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_roman.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Number of introductory pages that should receive Roman numerals.
        // Adjust this value according to the document structure.
        const int introPageCount = 5;

        // Load the PDF inside a using block to ensure proper disposal.
        using (Document doc = new Document(inputPath))
        {
            // Iterate over the first 'introPageCount' pages (1‑based indexing).
            for (int i = 1; i <= Math.Min(introPageCount, doc.Pages.Count); i++)
            {
                Page page = doc.Pages[i];

                // Create a PageNumberStamp for each page.
                PageNumberStamp stamp = new PageNumberStamp
                {
                    // Use uppercase Roman numerals (I, II, III, ...).
                    NumberingStyle = NumberingStyle.NumeralsRomanUppercase,

                    // Position the stamp at the bottom center of the page.
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Bottom,

                    // Optional margins to fine‑tune placement.
                    BottomMargin = 20,

                    // The format string must contain '#' which will be replaced
                    // by the page number according to the selected NumberingStyle.
                    Format = "#"
                };

                // Add the stamp to the current page.
                page.AddStamp(stamp);
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Roman page numbers added. Output saved to '{outputPath}'.");
    }
}