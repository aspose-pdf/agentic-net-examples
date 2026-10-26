using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string backgroundImgPath = "background.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(backgroundImgPath))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundImgPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare a reusable ImageStamp for the background image
            ImageStamp bgStamp = new ImageStamp(backgroundImgPath)
            {
                Background = true,               // place it behind existing content
                Opacity = 0.3f,                  // 30 % opacity
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                XIndent = 0,
                YIndent = 0
            };

            // Apply the stamp to every page, adjusting its size to the page dimensions
            foreach (Page page in pdfDoc.Pages)
            {
                bgStamp.Width = page.PageInfo.Width;
                bgStamp.Height = page.PageInfo.Height;
                // XIndent/YIndent are already 0, ensuring the stamp starts at the page origin
                page.AddStamp(bgStamp);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Background image applied and saved to '{outputPdfPath}'.");
    }
}