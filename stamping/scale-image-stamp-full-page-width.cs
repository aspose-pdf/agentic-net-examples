using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;   // for ImageStamp, HorizontalAlignment, VerticalAlignment
using System.Drawing;      // for System.Drawing.Image (fully qualified usage)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string stampImg  = "stamp.png";

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Load the image stream once per page
                using (FileStream imgStream = File.OpenRead(stampImg))
                {
                    // Create an ImageStamp from the stream
                    ImageStamp stamp = new ImageStamp(imgStream);

                    // Determine original image dimensions using System.Drawing.Image
                    using (System.Drawing.Image sysImg = System.Drawing.Image.FromStream(imgStream, false, false))
                    {
                        double imgWidth  = sysImg.Width;
                        double imgHeight = sysImg.Height;
                        double aspectRatio = imgHeight / imgWidth; // height / width

                        // Desired width = full page width (including page margins)
                        double pageWidth = page.PageInfo.Width;
                        double scaledHeight = pageWidth * aspectRatio;

                        // Set stamp size preserving aspect ratio
                        stamp.Width  = pageWidth;
                        stamp.Height = scaledHeight;
                    }

                    // Align the stamp to the top of the page (you can change alignment as needed)
                    stamp.HorizontalAlignment = HorizontalAlignment.Center;
                    stamp.VerticalAlignment   = VerticalAlignment.Top;

                    // Add the stamp to the current page
                    page.AddStamp(stamp);
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdf}'.");
    }
}