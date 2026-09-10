using System;
using System.Drawing;                     // needed for DefaultAppearance color
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;                // Facade API as requested

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // ---------------------------------------------------------------------
        // Create a minimal input PDF so the example is self‑contained.
        // ---------------------------------------------------------------------
        using (Document seed = new Document())
        {
            seed.Pages.Add();               // add a blank page
            seed.Save(inputPath);            // persist it for the next step
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the Facades editor (required by the task)
            using (PdfContentEditor editor = new PdfContentEditor(doc))
            {
                // Define the annotation rectangle (coordinates are in points)
                // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create DefaultAppearance with Helvetica, size 12, blue color.
                // The constructor expects System.Drawing.Color for the text color.
                DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Blue);

                // Create the FreeTextAnnotation on page 1 using the appearance.
                FreeTextAnnotation freeText = new FreeTextAnnotation(doc.Pages[1], rect, appearance)
                {
                    Contents = "Sample free‑text annotation",
                    // Optional: set border/color of the annotation box
                    Color = Aspose.Pdf.Color.LightGray
                };

                // Add the annotation to the page's annotation collection.
                doc.Pages[1].Annotations.Add(freeText);

                // Save the modified PDF.
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"Free‑text annotation added and saved to '{outputPath}'.");
    }
}
