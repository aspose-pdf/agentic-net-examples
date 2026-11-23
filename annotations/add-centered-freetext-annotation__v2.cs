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

        // Open the PDF with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Select the page to annotate (first page)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 600);

            // Build the required DefaultAppearance for FreeTextAnnotation
            var appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the FreeTextAnnotation with the appearance
            var freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                Contents = "This is a centered free text annotation."
            };

            // Set text alignment to center using the non‑obsolete property
            freeText.TextStyle.HorizontalAlignment = HorizontalAlignment.Center;

            // Add the annotation to the page
            page.Annotations.Add(freeText);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"FreeTextAnnotation added and saved to '{outputPath}'.");
    }
}
