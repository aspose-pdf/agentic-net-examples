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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define a rectangle at the top of the page
                // llx = 0, lly = page height - 50, urx = page width, ury = page height
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    0,
                    page.PageInfo.Height - 50,
                    page.PageInfo.Width,
                    page.PageInfo.Height);

                // Create the WatermarkAnnotation for this page
                WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect);

                // Prepare text state: bold font, size 24, red color
                TextState textState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica-Bold"),
                    FontSize = 24,
                    ForegroundColor = Color.Red,
                    FontStyle = FontStyles.Bold
                };

                // Set the watermark text (single line) with the defined text state
                watermark.SetTextAndState(new string[] { "Confidential" }, textState);

                // Optionally make the watermark semi‑transparent
                watermark.Opacity = 0.5f;

                // Add the annotation to the page
                page.Annotations.Add(watermark);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}