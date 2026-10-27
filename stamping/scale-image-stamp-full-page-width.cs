using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // ImageStamp, HorizontalAlignment, VerticalAlignment

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "stamped_output.pdf";
        const string stampImg = "stamp.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Obtain the original image dimensions using System.Drawing.Image (fully qualified to avoid ambiguity).
        double aspectRatio;
        using (System.Drawing.Image sysImg = System.Drawing.Image.FromFile(stampImg))
        {
            // aspectRatio = height / width
            aspectRatio = (double)sysImg.Height / sysImg.Width;
        }

        // Open the PDF document.
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based index).
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Desired stamp width = full page width.
                double pageWidth = page.PageInfo.Width;
                double stampWidth = pageWidth;
                double stampHeight = stampWidth * aspectRatio; // preserve aspect ratio

                // Create the image stamp and set its size and alignment.
                ImageStamp stamp = new ImageStamp(stampImg)
                {
                    Width = stampWidth,
                    Height = stampHeight,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top
                };

                // Add the stamp to the current page.
                page.AddStamp(stamp);
            }

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp applied to all pages and saved as '{outputPdf}'.");
    }
}
