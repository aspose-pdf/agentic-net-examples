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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the watermark text and its visual style
            string[] watermarkLines = new string[] { "CONFIDENTIAL" };
            TextState textState = new TextState
            {
                FontSize = 72,                         // large font size
                ForegroundColor = Color.Gray,          // gray color
                Font = FontRepository.FindFont("Helvetica") // optional font
            };

            // Apply the watermark to every page
            foreach (Page page in doc.Pages)
            {
                // Use the full page rectangle as the annotation bounds
                Aspose.Pdf.Rectangle rect = page.Rect;

                // Create the WatermarkAnnotation for the current page
                WatermarkAnnotation wm = new WatermarkAnnotation(page, rect)
                {
                    Opacity = 0.3, // 30% opacity (semi‑transparent)
                    // Optional: set alignment or other properties here
                };

                // Set the watermark text and its TextState
                wm.SetTextAndState(watermarkLines, textState);

                // Add the annotation to the page
                page.Annotations.Add(wm);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}