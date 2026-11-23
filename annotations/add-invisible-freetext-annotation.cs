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
            // Choose the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury) – use fully qualified Aspose.Pdf.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // FreeTextAnnotation requires a DefaultAppearance (font, size, color)
            // DefaultAppearance expects System.Drawing.Color, so we keep the System.Drawing namespace here
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create a FreeTextAnnotation with invisible flags (Print | NoView)
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                // Optional content displayed when the annotation is opened
                Contents = "Sample free text annotation",
                // Set the annotation's flags to make it printable but not visible in the viewer
                Flags = AnnotationFlags.Print | AnnotationFlags.NoView,
                // Background colour – use Aspose.Pdf.Color
                Color = Aspose.Pdf.Color.LightGray
            };

            // Add the annotation to the page
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with FreeTextAnnotation to '{outputPath}'.");
    }
}
