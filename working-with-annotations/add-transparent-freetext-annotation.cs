using System;
using System.IO;
using System.Drawing;                     // needed for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;                    // needed for DefaultAppearance

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: using block)
        using (Document doc = new Document(inputPath))
        {
            // Select the first page (page indexing is 1‑based)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create DefaultAppearance for the text (font, size, color)
            // Note: DefaultAppearance constructor requires System.Drawing.Color
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the free‑text annotation with transparent background
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                Contents = "Overlay note",                     // visible text
                Color    = Aspose.Pdf.Color.Transparent,      // background is transparent
                Opacity  = 0.3f,                               // make the annotation itself semi‑transparent
                // Remove border (optional)
                Border   = new Border(null) { Width = 0 }
            };

            // Add the annotation to the page
            page.Annotations.Add(freeText);

            // Save the modified PDF (lifecycle rule: using block ensures disposal)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation added. Saved to '{outputPath}'.");
    }
}