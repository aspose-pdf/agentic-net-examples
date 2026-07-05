using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a PageNumberStamp – default format is "#"
            PageNumberStamp pageNumberStamp = new PageNumberStamp();
            // Position the stamp at the bottom center of each page
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            pageNumberStamp.BottomMargin       = 20; // margin from bottom edge
            // Font size must be set via TextState (FontSize property on TextStamp is read‑only)
            pageNumberStamp.TextState.FontSize = 12;
            pageNumberStamp.TextState.ForegroundColor = Aspose.Pdf.Color.DarkGray;

            // Apply the stamp to all existing pages
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(pageNumberStamp);
            }

            // Example: insert a new blank page at position 2 (1‑based index)
            Page newPage = doc.Pages.Insert(2);
            // Add the page number stamp to the newly inserted page
            newPage.AddStamp(pageNumberStamp);

            // Update pagination for the whole collection.
            // This refreshes the page numbers on all pages, including any newly added ones.
            doc.Pages.UpdatePagination();

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page‑numbered PDF saved to '{outputPath}'.");
    }
}