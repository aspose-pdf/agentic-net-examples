using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the stamp will appear (coordinates are in points).
            // Fully qualify the Rectangle type to avoid ambiguity with System.Drawing.
            Aspose.Pdf.Rectangle stampRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a StampAnnotation on the first page.
            // The constructor takes the target page and the annotation rectangle.
            StampAnnotation stamp = new StampAnnotation(doc.Pages[1], stampRect)
            {
                // Text that appears when the user hovers over the stamp.
                Contents = "Important Section",
                // Background color of the stamp (used for highlighting).
                Color = Aspose.Pdf.Color.Yellow
            };

            // Set a custom border thickness.
            // Border requires the parent annotation in its constructor.
            stamp.Border = new Border(stamp) { Width = 3 };

            // Add the stamp annotation to the page's annotation collection.
            doc.Pages[1].Annotations.Add(stamp);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}