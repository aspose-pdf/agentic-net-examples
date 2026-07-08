using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class InsertPageNumbersWithSuperscript
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
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create a PageNumberStamp – default format is "#"
                PageNumberStamp stamp = new PageNumberStamp();

                // Position the stamp at the bottom‑right corner
                stamp.HorizontalAlignment = HorizontalAlignment.Right;
                stamp.VerticalAlignment   = VerticalAlignment.Bottom;

                // Adjust the bottom margin to raise the number (superscript effect)
                // Larger BottomMargin moves the stamp farther from the bottom edge.
                stamp.BottomMargin = 30; // increased from 20 for superscript look

                // Configure superscript appearance: smaller font size
                stamp.TextState.Font     = FontRepository.FindFont("Helvetica");
                stamp.TextState.FontSize = 8; // smaller than typical body text
                // NOTE: TextState does NOT have a TextRise property in this version.
                // The vertical offset is achieved via BottomMargin instead.

                // Optional: make the stamp fully opaque
                stamp.Opacity = 1.0f;

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers with superscript formatting saved to '{outputPath}'.");
    }
}
