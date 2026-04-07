using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using System.Drawing; // Required for DefaultAppearance constructor

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

        // Load the PDF document (deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create DefaultAppearance with Arial, 12‑point size, black color
            // DefaultAppearance.Font is read‑only; use the constructor overload.
            DefaultAppearance appearance = new DefaultAppearance("Arial", 12, System.Drawing.Color.Black);

            // Create the free‑text annotation on the page
            FreeTextAnnotation ft = new FreeTextAnnotation(page, rect, appearance)
            {
                Contents = "Sample free‑text annotation",
                // Optional visual styling
                Color = Aspose.Pdf.Color.Yellow
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(ft);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation added. Saved to '{outputPath}'.");
    }
}