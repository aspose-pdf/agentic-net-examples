using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_freetext.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Choose the page to annotate (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the free‑text annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create DefaultAppearance (font, size, color) – DefaultAppearance.Font is read‑only,
            // so we use the constructor that accepts font name, size and Aspose.Pdf.Color
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the FreeTextAnnotation on the selected page
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                // The visible text inside the annotation
                Contents = "Important note with callout",
                // Optional border color
                Color = Aspose.Pdf.Color.Blue
            };

            // Define a callout line with exactly three points:
            // 1. Start point (inside the annotation rectangle)
            // 2. Knee point (bend)
            // 3. End point (target location on the page)
            freeText.Callout = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(150, 525), // start (inside)
                new Aspose.Pdf.Point(200, 600), // knee
                new Aspose.Pdf.Point(250, 650)  // end (pointing to target)
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(freeText);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation with callout saved to '{outputPath}'.");
    }
}