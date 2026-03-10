using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class TooltipExample
{
    static void Main()
    {
        // Paths to the source and destination PDF files
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_tooltip.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Define the rectangle where the tooltip (text annotation) will be placed.
        // Use fully qualified type to avoid ambiguity with System.Drawing.Rectangle.
        // Coordinates are in points: lower‑left x, lower‑left y, upper‑right x, upper‑right y.
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing).
            Page page = doc.Pages[1];

            // Create a TextAnnotation (sticky note) on the specified page and rectangle.
            TextAnnotation tooltip = new TextAnnotation(page, rect)
            {
                // The title appears in the annotation's title bar (optional).
                Title = "Info",

                // The Contents property is shown as a tooltip when the mouse hovers over the icon.
                Contents = "This is a custom tooltip displayed on hover.",

                // Set the icon color (optional) – use Aspose.Pdf.Color for cross‑platform compatibility.
                Color = Aspose.Pdf.Color.Yellow,

                // Open = false means the popup is not shown by default; only the tooltip appears on hover.
                Open = false
            };

            // Add the annotation to the page's annotation collection.
            page.Annotations.Add(tooltip);

            // Save the modified document. The using block ensures the document remains alive until Save completes.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with tooltip saved to '{outputPath}'.");
    }
}