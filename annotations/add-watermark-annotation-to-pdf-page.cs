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
        const string outputPath = "watermarked.pdf";
        const string watermarkText = "Custom Watermark";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 9 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 9)
            {
                Console.Error.WriteLine("The document does not contain page 9.");
                return;
            }

            // Get page 9
            Page page = doc.Pages[9];

            // Define the rectangle covering the whole page (lower‑left x,y to upper‑right x,y)
            Rectangle rect = new Rectangle(
                0,                                   // lower‑left X
                0,                                   // lower‑left Y
                page.PageInfo.Width,                 // upper‑right X
                page.PageInfo.Height);               // upper‑right Y

            // Create the watermark annotation (no Text property – use SetTextAndState instead)
            WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
            {
                Opacity = 0.5 // 50 % opacity
            };

            // Define the visual style of the watermark text
            TextState textState = new TextState
            {
                FontSize = 36,
                ForegroundColor = Color.Gray // you can choose any Aspose.Pdf.Color
            };

            // Apply the text and its style to the annotation
            watermark.SetTextAndState(new[] { watermarkText }, textState);

            // Add the annotation to the page
            page.Annotations.Add(watermark);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
