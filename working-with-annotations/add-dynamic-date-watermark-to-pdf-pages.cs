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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Generate the dynamic date string for this page
                string dateString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // Define text appearance using core TextState (no Facades)
                TextState textState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12,
                    ForegroundColor = Color.Gray
                };

                // Position of the watermark annotation (left, bottom, right, top)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 120);

                // Create the WatermarkAnnotation and set its text
                WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect);
                watermark.SetTextAndState(new[] { dateString }, textState);
                watermark.Opacity = 0.5; // semi‑transparent

                // Add the annotation to the page
                page.Annotations.Add(watermark);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}