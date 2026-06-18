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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over each page and add a watermark annotation
            foreach (Page page in doc.Pages)
            {
                double pageWidth = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Define a rectangle at the top of the page (50 units high)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, pageHeight - 50, pageWidth, pageHeight);

                // Create the watermark annotation
                WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect);

                // Configure text appearance: bold, red, size 24
                TextState textState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica-Bold"),
                    FontSize = 24,
                    ForegroundColor = Aspose.Pdf.Color.Red
                };

                // Set the watermark text
                watermark.SetTextAndState(new[] { "Confidential" }, textState);

                // Optional: make the watermark semi‑transparent
                watermark.Opacity = 0.5;

                // Add the annotation to the page
                page.Annotations.Add(watermark);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}