using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // FontRepository, TextState, FontStyles

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Define a rectangle at the top of the page.
                // llx = 0, lly = page height - 50, urx = page width, ury = page height
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    0,
                    page.PageInfo.Height - 50,
                    page.PageInfo.Width,
                    page.PageInfo.Height);

                // Create the WatermarkAnnotation for the current page.
                WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect);

                // Configure the text appearance (bold font, size, color).
                TextState textState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica-Bold"),
                    FontSize = 24,
                    FontStyle = FontStyles.Bold,
                    ForegroundColor = Color.Red
                };

                // Set the watermark text.
                watermark.SetTextAndState(new[] { "Confidential" }, textState);

                // Add the annotation to the page.
                page.Annotations.Add(watermark);
            }

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}