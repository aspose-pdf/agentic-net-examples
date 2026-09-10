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
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define a rectangle at the top of the page
                // left = 0, bottom = page height - 50, right = page width, top = page height
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    0,
                    page.PageInfo.Height - 50,
                    page.PageInfo.Width,
                    page.PageInfo.Height);

                // Create the watermark annotation
                WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect);

                // Prepare text state: bold Helvetica, size 24, red color
                TextState textState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 24,
                    FontStyle = FontStyles.Bold,
                    ForegroundColor = Aspose.Pdf.Color.Red
                };

                // Set the watermark text
                watermark.SetTextAndState(new[] { "Confidential" }, textState);

                // Add the annotation to the page
                page.Annotations.Add(watermark);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}