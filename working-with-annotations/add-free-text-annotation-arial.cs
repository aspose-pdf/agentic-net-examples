using System;
using System.IO;
using System.Drawing; // Required for DefaultAppearance color
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create DefaultAppearance with Arial, 12‑pt, black color
            // Note: DefaultAppearance constructor requires System.Drawing.Color
            DefaultAppearance appearance = new DefaultAppearance("Arial", 12, System.Drawing.Color.Black);

            // Create the free‑text annotation on the page
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                Contents = "This is a free‑text annotation."
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation added and saved to '{outputPath}'.");
    }
}