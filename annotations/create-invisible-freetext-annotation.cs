using System;
using System.IO;
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
            // Ensure there is at least one page to attach the annotation
            Page page = doc.Pages[1];

            // Define the rectangle where the FreeTextAnnotation would appear
            // (coordinates are in points; lower‑left (llx,lly) to upper‑right (urx,ury))
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // FreeTextAnnotation requires a DefaultAppearance (font, size, colour)
            var appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the FreeTextAnnotation with the required appearance
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                // Set the annotation flags to make it invisible in the viewer but printable
                Flags = AnnotationFlags.Print | AnnotationFlags.NoView,
                // Optional content – required for a valid annotation
                Contents = "Invisible free‑text annotation",
                // Make the background transparent (Aspose.Pdf.Color)
                Color = Aspose.Pdf.Color.Transparent
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with invisible FreeTextAnnotation to '{outputPath}'.");
    }
}
