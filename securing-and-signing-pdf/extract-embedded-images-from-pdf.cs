using System;
using System.IO;
using System.Drawing.Imaging; // ImageFormat for PNG
using Aspose.Pdf;

class ExtractEmbeddedImages
{
    static void Main()
    {
        const string inputPdfPath = "signed_input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the signed PDF
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Iterate through all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    Page page = pdfDoc.Pages[pageNum];
                    int imageIndex = 1;

                    // Iterate over all images defined in the page resources
                    foreach (XImage xImg in page.Resources.Images)
                    {
                        // Build a unique file name for each extracted image
                        string imageFileName = $"page_{pageNum}_image_{imageIndex}.png";
                        string imagePath = Path.Combine(outputFolder, imageFileName);

                        // Save the image as PNG using the Stream overload (required by Aspose.Pdf)
                        using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                        {
                            xImg.Save(fs, ImageFormat.Png);
                        }

                        Console.WriteLine($"Saved: {imagePath}");
                        imageIndex++;
                    }
                }
            }

            Console.WriteLine("Image extraction completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
