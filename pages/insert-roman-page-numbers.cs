using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, Page, PageNumberStamp, etc.)
using Aspose.Pdf.Text;          // For NumberingStyle enum and TextState

class InsertRomanPageNumbers
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "output_roman.pdf";   // result PDF
        const int introPageCount = 5;                   // number of introductory pages to number with Roman numerals

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PageNumberStamp that will be placed on each page
            PageNumberStamp romanStamp = new PageNumberStamp();
            romanStamp.NumberingStyle = NumberingStyle.NumeralsRomanUppercase; // I, II, III...
            romanStamp.StartingNumber = 1;                                      // start at I
            romanStamp.HorizontalAlignment = HorizontalAlignment.Center;        // centered horizontally
            romanStamp.VerticalAlignment   = VerticalAlignment.Bottom;          // placed at bottom
            romanStamp.BottomMargin = 20;                                       // distance from bottom edge
            // Font size must be set via TextState (FontSize property on the stamp is read‑only)
            romanStamp.TextState.FontSize = 12;                                 // readable size
            romanStamp.TextState.ForegroundColor = Color.Black;                // black text

            // Apply the stamp only to the introductory pages
            for (int i = 1; i <= Math.Min(introPageCount, doc.Pages.Count); i++)
            {
                Page page = doc.Pages[i];   // 1‑based indexing
                page.AddStamp(romanStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Roman page numbers added. Output saved to '{outputPath}'.");
    }
}
