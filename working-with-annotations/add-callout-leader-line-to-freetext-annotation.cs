using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_free_text.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            Page page = doc.Pages[1];

            // Define the rectangle that bounds the free‑text annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create the free‑text annotation and set its properties
            FreeTextAnnotation freeText = new FreeTextAnnotation(doc, new DefaultAppearance())
            {
                Rect = rect,
                Contents = "This is a free‑text annotation with a leader line.",
                // Use the Justification enum defined in Aspose.Pdf.Annotations
                Justification = Justification.Center,
                // Leader line (callout) coordinates
                Callout = new Point[] { new Point(80, 620), new Point(100, 620) }
            };

            // Add the annotation to the page
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation with callout saved to '{outputPath}'.");
    }
}
