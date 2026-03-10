using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "paged_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a page number stamp. The format string must contain '#'
            // which will be replaced by the actual page number.
            PageNumberStamp pageNumberStamp = new PageNumberStamp("#");

            // Configure visual appearance of the stamp
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;   // center horizontally
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;    // place at bottom
            pageNumberStamp.BottomMargin       = 20;                           // 20 points margin from bottom
            // pageNumberStamp.FontSize = 12; // <-- removed: FontSize is read‑only
            pageNumberStamp.TextState.FontSize = 12; // set size via TextState
            pageNumberStamp.TextState.Font     = FontRepository.FindFont("Helvetica");
            pageNumberStamp.TextState.ForegroundColor = Color.Black;          // use Aspose.Pdf.Color
            pageNumberStamp.Opacity            = 0.8;                          // slight transparency

            // Add the stamp to each page in the document
            foreach (Page page in doc.Pages)
            {
                // The stamp will automatically replace '#' with the current page number
                page.AddStamp(pageNumberStamp);
            }

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}
