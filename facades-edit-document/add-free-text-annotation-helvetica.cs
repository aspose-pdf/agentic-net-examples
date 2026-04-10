using System;
using System.IO;
using System.Drawing;                     // System.Drawing.Color for blue color
using Aspose.Pdf;                         // Core PDF classes
using Aspose.Pdf.Annotations;             // FreeTextAnnotation and DefaultAppearance
using Aspose.Pdf.Text;                    // (optional) for text-related enums if needed

class Program
{
    static void Main()
    {
        // Path for the output PDF
        const string outputPath = "FreeTextAnnotation.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the free‑text annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle annotRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the default appearance: Helvetica font, size 12, blue color
            // DefaultAppearance constructor expects System.Drawing.Color
            DefaultAppearance appearance = new DefaultAppearance(
                "Helvetica",               // Font name
                12,                        // Font size
                System.Drawing.Color.Blue // Text color
            );

            // Instantiate the free‑text annotation on the specified page
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, annotRect, appearance)
            {
                // The visible text of the annotation
                Contents = "Sample free‑text annotation"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(freeText);

            // Save the modified document to the output file
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation added and saved to '{outputPath}'.");
    }
}