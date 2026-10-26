using System;
using System.IO;
using System.Drawing; // Required for DefaultAppearance colour
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
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Define the annotation rectangle (lower‑left x, lower‑right y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Build the required DefaultAppearance (font name, size, colour)
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create a free‑text annotation with the desired appearance
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                Contents = "This is a free‑text annotation.",
                // Light gray background (Annotation.Color is the background colour for FreeTextAnnotation)
                Color = Aspose.Pdf.Color.LightGray
                // Rounded corners are not directly supported by FreeTextAnnotation; they would require custom appearance streams.
            };

            // Optional: set a visible border colour (default is black)
            freeText.Border = new Border(freeText)
            {
                Width = 1,
                // Border style defaults to solid; can be set explicitly if desired
                // Style = BorderStyle.Solid
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation added and saved to '{outputPath}'.");
    }
}
