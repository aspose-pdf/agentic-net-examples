using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string xmlPath = "images.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML containing image references (e.g., <Image src="path/to/img.jpg"/>)
        XDocument xdoc = XDocument.Load(xmlPath);

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add each image to its own page
            foreach (var imgElem in xdoc.Descendants("Image"))
            {
                string imgPath = (string)imgElem.Attribute("src");
                if (string.IsNullOrEmpty(imgPath) || !File.Exists(imgPath))
                {
                    Console.Error.WriteLine($"Image file not found: {imgPath}");
                    continue;
                }

                // Add a new page (pages are 1‑based)
                Page page = pdfDoc.Pages.Add();

                // Load the image via a stream – Aspose.Pdf.Image has no ctor that takes a file path
                using (FileStream fs = new FileStream(imgPath, FileMode.Open, FileAccess.Read))
                {
                    Image img = new Image { ImageStream = fs };

                    // Scale the image to fit within page margins
                    img.FixWidth = page.PageInfo.Width - page.PageInfo.Margin.Left - page.PageInfo.Margin.Right;
                    img.FixHeight = page.PageInfo.Height - page.PageInfo.Margin.Top - page.PageInfo.Margin.Bottom;

                    // Add the image to the page
                    page.Paragraphs.Add(img);
                }
            }

            // Optimize resources: down‑sample high‑resolution images and apply JPEG compression
            OptimizationOptions opt = new OptimizationOptions();
            // Configure image compression options (properties available in the current Aspose.Pdf version)
            opt.ImageCompressionOptions.CompressImages = true;          // Enable image compression
            opt.ImageCompressionOptions.ImageQuality = 80;              // JPEG quality (0‑100)
            opt.ImageCompressionOptions.MaxResolution = 150;           // Target DPI after down‑sampling
            opt.ImageCompressionOptions.ResizeImages = true;           // Allow resizing/down‑sampling

            pdfDoc.OptimizeResources(opt);

            // Save the optimized PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Optimized PDF saved to '{outputPdf}'.");
    }
}
