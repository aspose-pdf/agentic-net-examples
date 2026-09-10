using System;
using System.IO;
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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create DefaultAppearance with Arial, 12‑point size, black color
            DefaultAppearance appearance = new DefaultAppearance("Arial", 12, System.Drawing.Color.Black);

            // Create the free‑text annotation on the page
            FreeTextAnnotation ft = new FreeTextAnnotation(page, rect, appearance)
            {
                Contents = "Free‑text annotation example"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(ft);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}