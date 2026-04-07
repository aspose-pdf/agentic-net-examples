using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // for DefaultAppearance
using System.Drawing; // required for DefaultAppearance constructor (text color)

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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Document has no pages.");
                return;
            }

            // Target the first page (page indexing is 1‑based)
            Page page = doc.Pages[1];

            // Define the rectangle where the free‑text annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create DefaultAppearance (constructor requires System.Drawing.Color)
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the FreeTextAnnotation on the specified page
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                // Set the visible text of the annotation
                Contents = "This is a free‑text annotation with a light gray background and rounded corners.",

                // Set the background (fill) color – LightGray
                Color = Aspose.Pdf.Color.LightGray,

                // Optional: set opacity if desired (0.0‑1.0)
                Opacity = 1.0
            };

            // Configure the border (rounded corners are not directly supported via Border.CornerRadius)
            // Use a standard rectangular border; for true rounded‑corner visual effect you would need to
            // draw a custom appearance stream (e.g., using Aspose.Pdf.Drawing) which is beyond the simple API.
            freeText.Border = new Border(freeText)
            {
                Width = 1,               // thin border
                Style = BorderStyle.Solid // explicit style (default is Solid)
                // CornerRadius property does not exist; omitted.
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(freeText);

            // Save the modified document (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation added and saved to '{outputPath}'.");
    }
}
