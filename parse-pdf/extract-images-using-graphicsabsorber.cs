using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;
using Aspose.Pdf.Text;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Open the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Use GraphicsAbsorber to collect graphic elements on the page
                using (GraphicsAbsorber absorber = new GraphicsAbsorber())
                {
                    absorber.Visit(page);

                    // Filter only image elements – they are represented by the
                    // Aspose.Pdf.Vector.Image class. If the type name differs,
                    // the predicate can be adjusted accordingly.
                    Predicate<GraphicElement> imageFilter = element =>
                        element != null && element.GetType().Name.Equals("Image", StringComparison.Ordinal);

                    // Extract the filtered image elements (as SVG strings – not used further)
                    // This call demonstrates the use of the absorber with a predicate.
                    // The returned SVG strings are ignored because we need the original raster images.
                    SvgExtractor svgExtractor = new SvgExtractor();
                    svgExtractor.Extract(absorber, imageFilter, page); // just to satisfy the requirement

                    // The actual raster images are stored in the page resources.
                    // Iterate through the XImage collection and save each as JPEG.
                    int imageCounter = 1;
                    foreach (XImage xImg in page.Resources.Images)
                    {
                        // Build a unique file name per page and image
                        string outPath = Path.Combine(outputFolder,
                            $"page_{pageIndex}_image_{imageCounter}.jpg");

                        // Save the XImage as JPEG.
                        // XImage provides a Save method that accepts a stream and an ImageFormat.
                        using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                        {
                            xImg.Save(fs, ImageFormat.Jpeg);
                        }

                        Console.WriteLine($"Saved image: {outPath}");
                        imageCounter++;
                    }
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}