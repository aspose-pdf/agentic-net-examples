using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Color in DefaultAppearance
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
            // Ensure the document has at least one page
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("Document contains no pages.");
                return;
            }

            // Get the first page (1‑based indexing)
            Page firstPage = doc.Pages[1];

            // Remove all existing annotations from the first page
            firstPage.Annotations.Clear();

            // Define the rectangle where the FreeTextAnnotation will appear
            // Use Aspose.Pdf.Rectangle for annotation bounds
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Build the required DefaultAppearance (font name, size, color)
            // The colour for DefaultAppearance must be a System.Drawing.Color
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create a new FreeTextAnnotation with the page, rectangle and appearance
            FreeTextAnnotation freeText = new FreeTextAnnotation(firstPage, rect, appearance)
            {
                // Set the visible text of the annotation
                Contents = "This is a new free‑text annotation."
            };

            // Set background colour (Annotation.Color) and border
            freeText.Color = Aspose.Pdf.Color.LightYellow; // background colour
            // Border must be created with the parent annotation and has no Color property
            freeText.Border = new Border(freeText)
            {
                Width = 1
            };
            // Border colour is taken from the annotation's Color property (already set above)

            // Add the annotation to the page's annotation collection
            firstPage.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations updated and saved to '{outputPath}'.");
    }
}
