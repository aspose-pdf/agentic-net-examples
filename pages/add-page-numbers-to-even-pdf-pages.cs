using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "even_page_numbers.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Apply numbering only to even pages
                if (i % 2 == 0)
                {
                    // Create a page number stamp with default format "#"
                    PageNumberStamp stamp = new PageNumberStamp();
                    stamp.HorizontalAlignment = HorizontalAlignment.Center;
                    stamp.VerticalAlignment   = VerticalAlignment.Bottom;
                    stamp.BottomMargin        = 20;                     // distance from bottom edge
                    stamp.TextState.FontSize  = 12;                     // font size
                    stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black; // text color

                    // Add the stamp to the current page
                    doc.Pages[i].AddStamp(stamp);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with page numbers on even pages: '{outputPath}'.");
    }
}