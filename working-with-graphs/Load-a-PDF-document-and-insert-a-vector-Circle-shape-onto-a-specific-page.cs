using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page index (1‑based). Here we use page 1.
            Page page = doc.Pages[1];

            // Define the rectangle that bounds the circle annotation.
            // Rectangle constructor: (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a CircleAnnotation on the selected page with the rectangle.
            CircleAnnotation circle = new CircleAnnotation(page, rect)
            {
                // Optional visual properties
                Color = Aspose.Pdf.Color.Blue,          // Border color
                InteriorColor = Aspose.Pdf.Color.LightGray // Fill color
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(circle);

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Circle annotation added and saved to '{outputPath}'.");
    }
}