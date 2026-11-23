using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // for TextState

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 20 pages
            if (doc.Pages.Count < 20)
            {
                Console.Error.WriteLine("Document does not contain pages 18‑20.");
                return;
            }

            // Opacity values for pages 18, 19, 20
            float[] opacities = { 0.3f, 0.5f, 0.7f };
            int startPage = 18;

            for (int i = 0; i < opacities.Length; i++)
            {
                int pageNumber = startPage + i;
                Page page = doc.Pages[pageNumber]; // 1‑based indexing

                // Define a rectangle that covers the whole page
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    0,
                    0,
                    page.PageInfo.Width,
                    page.PageInfo.Height);

                // Create the watermark annotation (no Text property exists)
                WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
                {
                    Color   = Aspose.Pdf.Color.Gray,
                    Opacity = opacities[i]
                };

                // Set the visible text using SetTextAndState
                watermark.SetTextAndState(
                    new[] { $"Watermark Page {pageNumber}" },
                    new TextState
                    {
                        FontSize = 36,
                        ForegroundColor = Aspose.Pdf.Color.Gray
                    });

                // Add the annotation to the page
                page.Annotations.Add(watermark);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
