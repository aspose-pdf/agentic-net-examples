using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_callout.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure we work with the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the FreeText annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a DefaultAppearance (font, size, color) for the annotation text
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, Aspose.Pdf.Color.Black);

            // Instantiate the FreeTextAnnotation on the page
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                // Set the visible text of the annotation
                Contents = "This is a callout annotation.",

                // Define the callout line points (x1,y1, x2,y2, x3,y3)
                Callout = new double[] { 120, 520, 150, 560, 200, 600 },

                // Optional: set a border and background color
                Border = new Border { Width = 1, Color = Aspose.Pdf.Color.DarkGray },
                Color  = Aspose.Pdf.Color.LightYellow
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(freeText);

            // Save the modified PDF (no SaveOptions needed for PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with FreeText callout annotation to '{outputPath}'.");
    }
}