using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Aspose.Pdf.Page page = doc.Pages[1];

            // Define the annotation rectangle (lower‑left x/y, upper‑right x/y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a DefaultAppearance with Helvetica, size 12, blue color (System.Drawing.Color)
            DefaultAppearance defaultAppearance = new DefaultAppearance(
                "Helvetica", 12, System.Drawing.Color.Blue);

            // Construct the FreeTextAnnotation using the constructor that accepts DefaultAppearance
            FreeTextAnnotation freeTextAnnotation = new FreeTextAnnotation(page, rect, defaultAppearance);

            // Set the visible text of the annotation
            freeTextAnnotation.Contents = "Sample free‑text annotation";

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(freeTextAnnotation);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation added and saved to '{outputPath}'.");
    }
}
