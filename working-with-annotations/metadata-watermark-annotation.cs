using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for proper disposal.
        using (Document doc = new Document(inputPath))
        {
            // Retrieve some metadata to encode in the barcode.
            // Here we use the document title; fallback to a default string if missing.
            string metadata = !string.IsNullOrEmpty(doc.Info.Title) ? doc.Info.Title : "UntitledDocument";

            // Define the rectangle where the watermark annotation will be placed.
            // Coordinates are in points (1/72 inch) with lower‑left origin.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 600);

            // Create the WatermarkAnnotation on the first page.
            Aspose.Pdf.Annotations.WatermarkAnnotation watermark =
                new Aspose.Pdf.Annotations.WatermarkAnnotation(doc.Pages[1], rect);

            // Set the annotation contents to the metadata string.
            // In a real scenario this string could be encoded as a barcode
            // using a barcode font; here we simply store the text.
            watermark.Contents = metadata;

            // Appearance settings.
            watermark.Color   = Aspose.Pdf.Color.Black;   // Text color.
            watermark.Opacity = 0.5;                      // Semi‑transparent.

            // Add the annotation to the page's annotation collection.
            doc.Pages[1].Annotations.Add(watermark);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}