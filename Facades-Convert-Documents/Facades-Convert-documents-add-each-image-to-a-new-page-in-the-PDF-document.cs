using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Expect two arguments: folder containing images and output PDF file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: <program> <images_folder> <output_pdf>");
                return;
            }

            string imagesFolder = args[0];
            string outputPdfPath = args[1];

            // Verify the images folder exists
            if (!Directory.Exists(imagesFolder))
            {
                Console.WriteLine($"Error: Images folder not found – {imagesFolder}");
                return;
            }

            // Ensure the output directory exists
            string outputDir = Path.GetDirectoryName(outputPdfPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Create a new empty PDF document
            Document pdfDocument = new Document();

            // Supported image extensions
            string[] supportedExt = { ".png", ".jpg", ".jpeg", ".bmp", ".gif" };

            // Enumerate image files in the folder
            foreach (string imagePath in Directory.GetFiles(imagesFolder))
            {
                string ext = Path.GetExtension(imagePath).ToLowerInvariant();
                if (Array.IndexOf(supportedExt, ext) < 0)
                    continue; // skip non‑image files

                // Add a new blank page for this image
                Page page = pdfDocument.Pages.Add();

                // Load the image using a stream (Aspose.Pdf.Image has no ctor that accepts a file path)
                using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    Image image = new Image { ImageStream = fs };

                    // Scale the image to fit the page while preserving aspect ratio
                    if (image.FixWidth == 0 && image.FixHeight == 0)
                    {
                        // Optional: set width/height to page dimensions (Aspose.Pdf will auto‑scale otherwise)
                        image.FixWidth = page.PageInfo.Width;
                        image.FixHeight = page.PageInfo.Height;
                    }

                    // Add the image to the page's content
                    page.Paragraphs.Add(image);
                }
            }

            // Save the resulting PDF
            pdfDocument.Save(outputPdfPath);

            Console.WriteLine($"Successfully created PDF with {pdfDocument.Pages.Count} pages at '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}