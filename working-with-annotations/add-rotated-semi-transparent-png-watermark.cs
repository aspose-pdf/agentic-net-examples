using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string logoPng = "logo.png";
        const string outputPdf = "watermarked.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(logoPng))
        {
            Console.Error.WriteLine($"Logo PNG not found: {logoPng}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Use the first page for the watermark
            Page page = doc.Pages[1];

            // Create an ImageStamp for the PNG logo
            ImageStamp stamp = new ImageStamp(logoPng)
            {
                // Desired size of the watermark (200x200 points)
                Width = 200,
                Height = 200,
                // Center the stamp on the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                // Semi‑transparent
                Opacity = 0.5,
                // Rotate 30 degrees clockwise
                RotateAngle = 30
            };

            // Add the stamp to the page
            page.AddStamp(stamp);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}
