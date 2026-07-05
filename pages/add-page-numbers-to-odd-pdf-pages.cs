using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // for TextState

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
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Apply numbering only to odd‑numbered pages
                if (i % 2 == 1)
                {
                    // Create a page number stamp; default format is "#"
                    PageNumberStamp stamp = new PageNumberStamp();

                    // Position the stamp at the bottom‑center of the page
                    stamp.HorizontalAlignment = HorizontalAlignment.Center;
                    stamp.VerticalAlignment   = VerticalAlignment.Bottom;
                    stamp.BottomMargin        = 20; // distance from the bottom edge

                    // Optional styling
                    stamp.TextState.FontSize        = 12;
                    stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                    // Add the stamp to the current page
                    doc.Pages[i].AddStamp(stamp);
                }
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Odd page numbers added and saved to '{outputPath}'.");
    }
}