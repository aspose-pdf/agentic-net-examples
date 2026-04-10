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
            // Retrieve some metadata to encode in the barcode.
            // Here we use the document title; replace with any metadata you need.
            string metaData = doc.Info.Title ?? "Untitled";

            // Define the rectangle where the watermark annotation will be placed.
            // This example places it in the centre of the first page.
            Page page = doc.Pages[1];
            double pageWidth  = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;
            double rectWidth  = pageWidth * 0.6;
            double rectHeight = 50; // height of the barcode text
            double llx = (pageWidth  - rectWidth) / 2;
            double lly = (pageHeight - rectHeight) / 2;
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, llx + rectWidth, lly + rectHeight);

            // Create the WatermarkAnnotation
            WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
            {
                Opacity = 0.7,               // semi‑transparent
                Color   = Aspose.Pdf.Color.LightGray
            };

            // Prepare a TextState that uses a barcode font.
            // If a barcode font (e.g., Code128) is not installed, fall back to a regular font.
            Font barcodeFont;
            try
            {
                barcodeFont = FontRepository.FindFont("Code128");
            }
            catch
            {
                barcodeFont = FontRepository.FindFont("Helvetica");
            }

            TextState ts = new TextState
            {
                Font        = barcodeFont,
                FontSize    = 48,
                ForegroundColor = Aspose.Pdf.Color.Black,
                // Render the text as a barcode by using the barcode font.
                // No additional encoding is required.
            };

            // Set the barcode text (metadata) into the annotation.
            // SetTextAndState expects an array of strings (one per line) and a TextState.
            watermark.SetTextAndState(new[] { metaData }, ts);

            // Add the annotation to the page.
            page.Annotations.Add(watermark);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}