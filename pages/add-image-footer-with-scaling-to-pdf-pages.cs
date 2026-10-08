using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string footerImagePath = "footer.png";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(footerImagePath))
        {
            Console.Error.WriteLine($"Footer image not found: {footerImagePath}");
            return;
        }

        // Load the original image to obtain its dimensions.
        using (System.Drawing.Image img = System.Drawing.Image.FromFile(footerImagePath))
        {
            const float scaleFactor = 0.5f; // 50% of original size
            float scaledWidth = img.Width * scaleFactor;
            float scaledHeight = img.Height * scaleFactor;

            // Load the PDF document inside a using block for deterministic disposal.
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Create an ImageStamp for the footer image.
                ImageStamp footerStamp = new ImageStamp(footerImagePath)
                {
                    // Position the stamp at the bottom center of each page.
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Bottom,
                    // YIndent works as a bottom margin when VerticalAlignment is Bottom.
                    YIndent = 10, // distance from the bottom edge (points)
                    Background = false,
                    // Apply scaling via Width/Height.
                    Width = scaledWidth,
                    Height = scaledHeight
                };

                // Apply the stamp to every page in the document.
                foreach (Page page in pdfDoc.Pages)
                {
                    page.AddStamp(footerStamp);
                }

                // Save the modified PDF.
                pdfDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Footer added and saved to '{outputPdfPath}'.");
    }
}
