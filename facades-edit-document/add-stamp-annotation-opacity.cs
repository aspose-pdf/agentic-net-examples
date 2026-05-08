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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Choose the page to place the annotation (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle where the stamp annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a rubber‑stamp annotation on the selected page
            StampAnnotation stamp = new StampAnnotation(page, rect)
            {
                // Set the desired opacity (0.75 = 75% opaque)
                Opacity = 0.75
                // Note: BlendMode is not supported on StampAnnotation in the current Aspose.PDF version.
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(stamp);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation added and saved to '{outputPath}'.");
    }
}
