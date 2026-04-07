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
        const string watermarkText = "CONFIDENTIAL";

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

            // Determine page dimensions
            double pageWidth  = lastPage.PageInfo.Width;
            double pageHeight = lastPage.PageInfo.Height;

            // Define the size of the text rectangle
            const double rectWidth  = 150; // width of the text box
            const double rectHeight = 30;  // height of the text box
            const double margin     = 20; // distance from page edges

            // Position rectangle at bottom‑right corner
            double llx = pageWidth  - margin - rectWidth; // lower‑left X
            double lly = margin;                         // lower‑left Y
            double urx = pageWidth  - margin;            // upper‑right X
            double ury = margin + rectHeight;            // upper‑right Y

            // Create a text paragraph, set its rectangle and rotation
            TextParagraph paragraph = new TextParagraph
            {
                Rectangle = new Aspose.Pdf.Rectangle(llx, lly, urx, ury),
                Rotation  = 45 // rotate 45 degrees
            };

            // Add the desired text line
            paragraph.AppendLine(watermarkText);

            // Append the paragraph to the page using TextBuilder
            TextBuilder builder = new TextBuilder(lastPage);
            builder.AppendParagraph(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated text added and saved to '{outputPath}'.");
    }
}