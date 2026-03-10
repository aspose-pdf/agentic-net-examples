using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // for FormattedText

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";
        // TeX expression to be used as watermark
        const string texExpression = @"$\int_{0}^{\infty} e^{-x} dx$";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define a rectangle that covers the whole page
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, page.PageInfo.Width, page.PageInfo.Height);

            // Create a WatermarkAnnotation on the page
            WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect);

            // Create FormattedText containing the TeX markup
            FormattedText formattedText = new FormattedText(texExpression);

            // Assign the formatted text to the annotation
            watermark.SetText(formattedText);

            // Optional visual styling
            watermark.Color = Aspose.Pdf.Color.Gray;   // watermark color
            watermark.Opacity = 0.3;                    // semi‑transparent

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(watermark);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark annotation added. Saved to '{outputPath}'.");
    }
}