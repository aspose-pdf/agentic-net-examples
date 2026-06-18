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

        // Load the PDF document (using rule for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the last page (1‑based indexing)
            int lastPageIndex = doc.Pages.Count;
            Page lastPage = doc.Pages[lastPageIndex];

            // Page dimensions
            double pageWidth  = lastPage.PageInfo.Width;
            double pageHeight = lastPage.PageInfo.Height;

            // Define a rectangle at the bottom‑right corner with a margin
            double margin      = 20;
            double rectWidth   = 200;
            double rectHeight  = 50;
            double llx = pageWidth - margin - rectWidth; // lower‑left X
            double lly = margin;                         // lower‑left Y
            double urx = pageWidth - margin;             // upper‑right X
            double ury = margin + rectHeight;            // upper‑right Y

            // Create a text paragraph, set its rectangle and rotation
            TextParagraph paragraph = new TextParagraph
            {
                Rectangle = new Aspose.Pdf.Rectangle(llx, lly, urx, ury),
                Rotation  = 45 // rotate 45 degrees
            };
            paragraph.AppendLine("Rotated Text");

            // Append the paragraph to the page using TextBuilder
            TextBuilder builder = new TextBuilder(lastPage);
            builder.AppendParagraph(paragraph);

            // Save the modified PDF (using rule for saving)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated text added to the bottom‑right of the last page and saved as '{outputPath}'.");
    }
}