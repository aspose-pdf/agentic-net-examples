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
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (change index if needed)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a DefaultAppearance that defines font, size and color (System.Drawing.Color required)
            var appearance = new DefaultAppearance("Arial", 12, System.Drawing.Color.Black);

            // Create a free‑text annotation on the page using the correct constructor order
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                // Text displayed inside the annotation
                Contents = "This is a free‑text annotation."
            };

            // Attach the annotation to the page
            page.Annotations.Add(freeText);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation added and saved to '{outputPath}'.");
    }
}
