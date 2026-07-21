using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;
using System.Drawing; // for System.Drawing.Color used by DefaultAppearance

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "free_text_annotation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create DefaultAppearance (font name, size, text color) – uses System.Drawing.Color
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Blue);

            // Create the FreeTextAnnotation with a callout (exactly three points)
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                Contents = "Important note with callout",
                Color = Aspose.Pdf.Color.Yellow, // border color
                Opacity = 0.8,
                Callout = new Aspose.Pdf.Point[]
                {
                    new Aspose.Pdf.Point(150, 550), // start point (inside annotation)
                    new Aspose.Pdf.Point(200, 620), // knee point (bend)
                    new Aspose.Pdf.Point(250, 650)  // end point (target location)
                }
            };

            // Add the annotation to the page
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation saved to '{outputPath}'.");
    }
}