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
        const string text       = "Rotated Text";
        const float  rotation   = 45f;          // degrees
        const float  margin     = 20f;          // distance from page edges
        const float  rectWidth  = 200f;         // width of the text rectangle
        const float  rectHeight = 50f;          // height of the text rectangle

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the last page (1‑based indexing)
            Page lastPage = doc.Pages[doc.Pages.Count];

            // Determine rectangle positioned at the bottom‑right corner
            // Page dimensions are accessed via PageInfo (double values)
            double pageWidth  = lastPage.PageInfo.Width;
            double pageHeight = lastPage.PageInfo.Height;

            // Cast to float because Aspose.Pdf.Rectangle constructor expects float arguments
            float llx = (float)(pageWidth  - margin - rectWidth);
            float lly = (float)margin;
            float urx = (float)(pageWidth  - margin);
            float ury = (float)(margin + rectHeight);

            // Bottom‑right rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Create a TextParagraph, set its rectangle, rotation and content
            TextParagraph paragraph = new TextParagraph
            {
                Rectangle = rect,
                Rotation  = rotation
            };
            paragraph.AppendLine(text);

            // Use TextBuilder to append the paragraph to the page
            TextBuilder builder = new TextBuilder(lastPage);
            builder.AppendParagraph(paragraph);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated text added and saved to '{outputPath}'.");
    }
}
