using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_page_numbers.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the standard load rule)
        using (Document doc = new Document(inputPath))
        {
            // Create a page number stamp with a format that pads single‑digit pages with a leading zero.
            // The placeholder '#' is replaced by the page number; prefixing with '0' adds zero padding.
            PageNumberStamp pageNumberStamp = new PageNumberStamp("0#");

            // Position the stamp at the bottom‑center of each page.
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            pageNumberStamp.BottomMargin        = 20; // distance from the bottom edge

            // Optional styling (font, size, color)
            pageNumberStamp.TextState.Font         = FontRepository.FindFont("Helvetica");
            pageNumberStamp.TextState.FontSize     = 12;
            pageNumberStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Apply the stamp to every page in the document.
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(pageNumberStamp);
            }

            // Save the modified PDF (using the standard save rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}