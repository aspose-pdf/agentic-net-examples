using System;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Create a minimal PDF if it does not already exist.
        if (!System.IO.File.Exists(inputPath))
        {
            using (Document seed = new Document())
            {
                seed.Pages.Add(); // add a blank page
                seed.Save(inputPath);
            }
        }

        // Initialize the content editor and bind the source PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Define the rectangle area for the annotation.
            // System.Drawing.Rectangle expects (x, y, width, height).
            // The desired PDF coordinates are lower‑left (100,500) and upper‑right (300,600).
            // Hence width = 300‑100 = 200, height = 600‑500 = 100.
            System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(100, 500, 200, 100);

            // Add a rectangle (square‑circle) annotation with light‑gray fill and 1 pt border.
            // Parameters: rectangle, contents, fill/border colour, true => square, page number (1‑based), border width.
            editor.CreateSquareCircle(
                annotRect,
                string.Empty,
                System.Drawing.Color.LightGray,
                true,
                1,
                1);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Rectangle annotation added and saved to '{outputPath}'.");
    }
}
