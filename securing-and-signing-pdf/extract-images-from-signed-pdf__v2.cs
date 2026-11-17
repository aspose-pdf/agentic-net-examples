using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input signed PDF
        const string inputPdfPath = "signed_input.pdf";

        // Base folder where page‑specific folders will be created
        const string outputRootFolder = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the root output folder exists
        Directory.CreateDirectory(outputRootFolder);

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Create a folder for the current page (e.g., Page_1, Page_2, ...)
                string pageFolder = System.IO.Path.Combine(outputRootFolder, $"Page_{pageIndex}");
                Directory.CreateDirectory(pageFolder);

                int imageCounter = 1;

                // Iterate over all images on the page.
                foreach (XImage img in page.Resources.Images)
                {
                    // Determine a file name for the image.
                    string extension = GetImageExtension(img);
                    string imageFileName = $"Image_{imageCounter}{extension}";
                    string imagePath = System.IO.Path.Combine(pageFolder, imageFileName);

                    // Save the image to the file system using a stream (XImage.Save expects a Stream).
                    using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }

                    Console.WriteLine($"Saved {imagePath}");
                    imageCounter++;
                }

                // If a page contains no images, optionally inform the user.
                if (imageCounter == 1)
                {
                    Console.WriteLine($"No images found on page {pageIndex}.");
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }

    // Helper method to provide a safe default file extension for extracted images.
    // The ImageInfo property is not available in older Aspose.Pdf versions, so we default to PNG.
    private static string GetImageExtension(XImage img)
    {
        // If a more sophisticated format detection is required, inspect the raw bytes after saving.
        // For now, PNG works for the majority of raster images extracted from PDFs.
        return ".png";
    }
}
