using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_page_numbers.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add a superscript‑style page number stamp to each page
            foreach (Page page in doc.Pages)
            {
                // Default format "#" will be replaced by the page number
                PageNumberStamp stamp = new PageNumberStamp();

                // Position the stamp in the bottom‑right corner
                stamp.HorizontalAlignment = HorizontalAlignment.Right;
                stamp.VerticalAlignment   = VerticalAlignment.Bottom;
                stamp.BottomMargin        = 15; // slightly higher to mimic superscript
                stamp.RightMargin         = 20; // distance from right edge

                // Superscript styling (smaller font size)
                stamp.TextState.FontSize = 8; // smaller than normal text
                // The Rise property is not available in this version of Aspose.Pdf;
                // adjusting BottomMargin provides a visual superscript effect.
                stamp.TextState.Font = FontRepository.FindFont("Helvetica");
                stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Apply the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with superscript page numbers to '{outputPath}'.");
    }
}
