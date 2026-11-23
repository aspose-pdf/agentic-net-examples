using System;
using System.IO;
using System.Drawing; // for System.Drawing.Color
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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Define the rectangle where the free‑text annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 550);

            // Build the required DefaultAppearance (font, size, colour)
            var appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the FreeTextAnnotation with page, rectangle and appearance
            var freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                // Set the displayed text
                Contents = "Centered annotation text",
                // Center the text inside the annotation
                Justification = Justification.Center
            };

            // Optionally set a background colour (uses the inherited Color property)
            // freeText.Color = Color.LightGray;

            // Add the annotation to the page
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"FreeTextAnnotation added and saved to '{outputPath}'.");
    }
}
