using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;          // for DefaultAppearance
using System.Drawing;          // for System.Drawing.Color (required by DefaultAppearance)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "free_text_with_callout.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, modify it, and save – all within a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the free‑text annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a DefaultAppearance – note the constructor requires System.Drawing.Color
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the FreeTextAnnotation with the page, rectangle, and appearance
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance);
            freeText.Contents = "This is a free‑text annotation with a callout.";
            freeText.Color = Aspose.Pdf.Color.LightYellow; // background color

            // Set the border after the annotation instance is created (Border requires the parent annotation)
            freeText.Border = new Border(freeText) { Width = 1 };

            // Define a callout line – exactly three points are required:
            //   1. Start point (inside the annotation rectangle)
            //   2. Knee point (where the line bends)
            //   3. End point (the point the callout points to)
            freeText.Callout = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(150, 525), // start (inside the annotation)
                new Aspose.Pdf.Point(200, 580), // knee (bend)
                new Aspose.Pdf.Point(250, 620)  // end (target location on the page)
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation with callout saved to '{outputPath}'.");
    }
}
