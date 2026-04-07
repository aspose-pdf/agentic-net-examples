using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define a rectangle positioned at the top of the page
                // (llx, lly) = (0, page height - 50)
                // (urx, ury) = (page width, page height)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    0,
                    page.PageInfo.Height - 50,
                    page.PageInfo.Width,
                    page.PageInfo.Height);

                // Create a WatermarkAnnotation for the current page
                WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect);

                // Configure the text appearance (bold font, size, color)
                TextState textState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica-Bold"),
                    FontSize = 24,
                    ForegroundColor = Aspose.Pdf.Color.Red
                };

                // Set the watermark text and its visual state
                watermark.SetTextAndState(new string[] { "Confidential" }, textState);

                // Optional: make the watermark semi‑transparent
                watermark.Opacity = 0.5;

                // Add the annotation to the page
                page.Annotations.Add(watermark);
            }

            // Save the modified PDF (lifecycle rule: save within using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}