using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Select the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle that will contain the free‑text annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Build the default appearance (font, size, color)
            DefaultAppearance appearance = new DefaultAppearance(
                "Helvetica",
                12,
                System.Drawing.Color.Black);

            // Create the free‑text annotation with page, rectangle and appearance
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance);

            // Set the visible text of the annotation
            freeText.Contents = "Important note";

            // Define a callout consisting of three points: start, knee, end (coordinates in points)
            freeText.Callout = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(150, 620),
                new Aspose.Pdf.Point(200, 580),
                new Aspose.Pdf.Point(250, 540)
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation with callout saved to '{outputPath}'.");
    }
}
