using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the first WatermarkAnnotation from the first page
            WatermarkAnnotation original = doc.Pages[1]
                .Annotations
                .OfType<WatermarkAnnotation>()
                .FirstOrDefault();

            if (original == null)
            {
                Console.Error.WriteLine("No WatermarkAnnotation found on the first page.");
                return;
            }

            // Base rectangle of the original annotation (use the Rect property)
            Aspose.Pdf.Rectangle baseRect = original.Rect;

            // Apply cloned watermark to each subsequent page with a vertical offset
            for (int i = 2; i <= doc.Pages.Count; i++)
            {
                // Clone the original annotation
                WatermarkAnnotation clone = (WatermarkAnnotation)original.Clone();

                // Calculate a new rectangle shifted down by 50 points per page
                double offset = 50.0 * (i - 1);
                Aspose.Pdf.Rectangle newRect = new Aspose.Pdf.Rectangle(
                    baseRect.LLX,
                    baseRect.LLY - offset,
                    baseRect.URX,
                    baseRect.URY - offset);

                // Assign the adjusted rectangle to the clone (use Rect property)
                clone.Rect = newRect;

                // Add the cloned annotation to the current page
                doc.Pages[i].Annotations.Add(clone);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
