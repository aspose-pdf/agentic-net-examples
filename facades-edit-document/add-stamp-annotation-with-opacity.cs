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
            // Define the annotation rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a rubber stamp annotation on the first page
            StampAnnotation stamp = new StampAnnotation(doc.Pages[1], rect)
            {
                // Set the desired opacity (0.0 = fully transparent, 1.0 = fully opaque)
                Opacity = 0.75,

                // Optional: set a visible color or icon if needed
                Color = Aspose.Pdf.Color.Red,
                Contents = "Sample Stamp"
            };

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(stamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation added with opacity 0.75. Saved to '{outputPath}'.");
    }
}
