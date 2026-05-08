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

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve a piece of metadata to encode in the barcode.
            // Here we use the document title; fallback to a default value if empty.
            string metaValue = doc.Info.Title;
            if (string.IsNullOrWhiteSpace(metaValue))
                metaValue = "DEFAULT";

            // For demonstration we treat the metadata string as a barcode value.
            // In a real scenario you could generate an actual barcode image using a
            // separate library and embed it, but WatermarkAnnotation only supports
            // text content, so we use the string directly.
            string barcodeText = metaValue;

            // Define the rectangle where the watermark will be placed (in points).
            // Rectangle constructor: (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 400, 500, 500);

            // Create a WatermarkAnnotation on the first page.
            Page page = doc.Pages[1];
            WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
            {
                // Set the text that represents the barcode.
                Contents = barcodeText,

                // Visual appearance settings.
                Color   = Aspose.Pdf.Color.Gray,
                Opacity = 0.3,
                // Rotation is not supported on WatermarkAnnotation; omit or use a different annotation type if rotation is required.
            };

            // Add the annotation to the page.
            page.Annotations.Add(watermark);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
