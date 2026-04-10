using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page to which the annotation will be added (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle area for the stamp annotation (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a StampAnnotation on the selected page
            StampAnnotation stamp = new StampAnnotation(page, rect)
            {
                // Set the opacity to 0.75 (75% visible)
                Opacity = 0.75,

                // Optional: set the blend mode if the property exists.
                // The BlendMode property is not listed in the core documentation,
                // but many Aspose.Pdf versions expose it. Uncomment the line below
                // if your version supports it.
                // BlendMode = BlendMode.Multiply
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(stamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation with opacity 0.75 saved to '{outputPath}'.");
    }
}