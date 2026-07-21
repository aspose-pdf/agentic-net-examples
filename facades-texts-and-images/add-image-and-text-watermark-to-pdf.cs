using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "watermarked.pdf";
        const string imagePath = "logo.png";
        const string watermarkText = "CONFIDENTIAL";

        if (!File.Exists(inputPdf) || !File.Exists(imagePath))
        {
            Console.Error.WriteLine("Missing input PDF or image file.");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages and add the image + text watermark
            foreach (Page page in doc.Pages)
            {
                // ------------------------------------------------------------
                // 1. Add the image as a semi‑transparent background using ImageStamp
                // ------------------------------------------------------------
                ImageStamp imgStamp = new ImageStamp(imagePath)
                {
                    // Stretch the image to cover the whole page
                    Width = page.PageInfo.Width,
                    Height = page.PageInfo.Height,
                    // Make the image semi‑transparent (0 = fully transparent, 1 = opaque)
                    Opacity = 0.2f,
                    // Place it behind the page content
                    Background = true
                };
                page.AddStamp(imgStamp);

                // ------------------------------------------------------------
                // 2. Add the text watermark on top of the image
                // ------------------------------------------------------------
                TextFragment tf = new TextFragment(watermarkText)
                {
                    // Center the text on the page using alignment properties
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                tf.TextState.Font = FontRepository.FindFont("Arial");
                tf.TextState.FontSize = 72;
                tf.TextState.FontStyle = FontStyles.Bold;
                // Gray with ~30% opacity (alpha = 77 out of 255)
                tf.TextState.ForegroundColor = Color.FromArgb(77, 128, 128, 128);
                // RenderingMode defaults to Fill, so no explicit setting needed
                page.Paragraphs.Add(tf);
            }

            // Save the watermarked PDF
            doc.Save(outputPdf);
            Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
        }
    }
}
