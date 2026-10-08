using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPdf = "result.pdf";

        // List of allowed image extensions (case‑insensitive)
        string[] allowedExt = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tif", ".tiff" };

        // Example image files – replace with your own paths
        string[] imageFiles = {
            "image1.jpg",
            "image2.png",
            "image3.tif"
        };

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document())
        {
            foreach (string imgPath in imageFiles)
            {
                if (!File.Exists(imgPath))
                {
                    Console.Error.WriteLine($"File not found: {imgPath}");
                    continue;
                }

                // Validate the image format by its file extension
                string ext = Path.GetExtension(imgPath);
                if (Array.Exists(allowedExt, e => e.Equals(ext, StringComparison.OrdinalIgnoreCase)))
                {
                    // Add a new page for each image (or reuse a page as needed)
                    Page page = pdfDoc.Pages.Add();

                    // Create an Image object and bind the file stream
                    using (FileStream imgStream = new FileStream(imgPath, FileMode.Open, FileAccess.Read))
                    {
                        Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image
                        {
                            ImageStream = imgStream,
                            // Fit the image to the page size while preserving aspect ratio
                            // (optional – adjust as required)
                            // Width = page.PageInfo.Width,
                            // Height = page.PageInfo.Height,
                            // HorizontalAlignment = HorizontalAlignment.Center,
                            // VerticalAlignment = VerticalAlignment.Center
                        };
                        page.Paragraphs.Add(pdfImage);
                    }
                }
                else
                {
                    Console.Error.WriteLine($"Unsupported image format: {imgPath}");
                }
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created: {outputPdf}");
    }
}
