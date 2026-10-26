using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

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
            // Select the page to annotate (first page in this example)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (lower‑left X,Y and upper‑right X,Y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Build the default appearance required by FreeTextAnnotation
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create a free‑text annotation with transparent background
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                // Text displayed inside the annotation
                Contents = "Note: Review this section.",

                // Make the background fully transparent
                Color = Aspose.Pdf.Color.Transparent,
                Opacity = 0.0
            };

            // Hide the border (optional) – Border requires the owning annotation instance
            freeText.Border = new Border(freeText) { Width = 0 };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation added and saved to '{outputPath}'.");
    }
}
