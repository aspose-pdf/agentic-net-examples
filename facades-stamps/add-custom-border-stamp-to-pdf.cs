using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;   // required for Facades API (if needed for other operations)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle area (llx, lly, urx, ury) where the stamp will appear.
            // Adjust these coordinates as needed for your specific highlight region.
            const double llx = 100;   // lower‑left X
            const double lly = 500;   // lower‑left Y
            const double urx = 400;   // upper‑right X
            const double ury = 550;   // upper‑right Y

            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a TextAnnotation that will act as the visual stamp.
                // The annotation uses Aspose.Pdf.Rectangle (fully qualified) to avoid ambiguity.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
                TextAnnotation annotation = new TextAnnotation(page, rect)
                {
                    // Optional: set a title and contents for accessibility / tooltip
                    Title    = "Important Section",
                    Contents = "Highlighted area",
                    // Set the border color (the annotation's own Color property controls border color)
                    Color    = Aspose.Pdf.Color.FromRgb(1.0, 0.0, 0.0) // red border
                };

                // Configure the border thickness.
                // Border requires the parent annotation instance in its constructor.
                annotation.Border = new Border(annotation) { Width = 2 }; // 2‑point thick border

                // Add the annotation to the page.
                page.Annotations.Add(annotation);
            }

            // Save the modified PDF. No PreSave() call is required.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}