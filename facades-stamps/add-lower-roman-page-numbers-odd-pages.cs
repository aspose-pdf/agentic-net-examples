using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // for FontRepository and TextState

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_odd_roman.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                // Apply the stamp only to odd‑numbered pages
                if (pageNumber % 2 == 1)
                {
                    // Create a PageNumberStamp (inherits TextStamp)
                    PageNumberStamp stamp = new PageNumberStamp();

                    // Use lower‑case Roman numerals
                    stamp.NumberingStyle = NumberingStyle.NumeralsRomanLowercase;

                    // The placeholder '#' will be replaced by the page number
                    stamp.Format = "#";

                    // Position the stamp at the bottom centre of the page
                    stamp.HorizontalAlignment = HorizontalAlignment.Center;
                    stamp.VerticalAlignment   = VerticalAlignment.Bottom;
                    stamp.BottomMargin        = 20;   // optional margin from the bottom edge

                    // Define visual appearance
                    stamp.TextState.Font         = FontRepository.FindFont("Helvetica");
                    stamp.TextState.FontSize     = 12;
                    stamp.TextState.ForegroundColor = Color.Black;

                    // Add the stamp to the current page
                    doc.Pages[pageNumber].AddStamp(stamp);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers (lower‑roman) added to odd pages. Saved as '{outputPath}'.");
    }
}