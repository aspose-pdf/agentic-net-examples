using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TextBuilder, TextParagraph, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string watermarkText = "Rotated Text";

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

            // Define a rectangle at the bottom‑right corner
            // Example: 200 units wide, 50 units high, 20 units margin from edges
            double rectWidth  = 200;
            double rectHeight = 50;
            double margin     = 20;

            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                pageWidth  - margin - rectWidth, // lower‑left X
                margin,                         // lower‑left Y
                pageWidth  - margin,            // upper‑right X
                margin + rectHeight);           // upper‑right Y

            // Create a text paragraph, set its rectangle and rotation
            TextParagraph paragraph = new TextParagraph
            {
                Rectangle = rect,
                Rotation  = 45 // rotate 45 degrees (any angle you need)
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