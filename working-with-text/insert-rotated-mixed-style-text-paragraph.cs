using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document must contain at least three pages.");
                return;
            }

            // Get the third page (1‑based indexing)
            Page page = doc.Pages[3];

            // Create a TextParagraph
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle where the paragraph will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(100, 500, 400, 600);

            // Rotate the paragraph by 45 degrees
            paragraph.Rotation = 45;

            // Build a TextFragment with mixed font styles
            TextFragment mixedFragment = new TextFragment();

            // First segment: bold font
            TextSegment boldSegment = new TextSegment("Bold Text");
            boldSegment.TextState.Font = FontRepository.FindFont("Helvetica-Bold");
            boldSegment.TextState.FontSize = 14;
            boldSegment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            mixedFragment.Segments.Add(boldSegment);

            // Second segment: italic font
            TextSegment italicSegment = new TextSegment(" and Italic Text");
            italicSegment.TextState.Font = FontRepository.FindFont("Helvetica-Oblique");
            italicSegment.TextState.FontSize = 14;
            italicSegment.TextState.ForegroundColor = Aspose.Pdf.Color.Green;
            mixedFragment.Segments.Add(italicSegment);

            // Append the mixed‑style fragment as a line in the paragraph
            paragraph.AppendLine(mixedFragment);

            // Use TextBuilder to place the paragraph on the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendParagraph(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}