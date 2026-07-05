using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "encrypted_input.pdf";
        const string userPwd   = "userPassword";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Open the encrypted PDF with the user password
            using (Document doc = new Document(inputPdf, userPwd))
            {
                // Decrypt the document (required before accessing resources)
                doc.Decrypt();

                // Iterate over all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    Page page = doc.Pages[pageNum];
                    int imgIndex = 1;

                    // Iterate over each image resource on the page
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Build a unique file name for each extracted image
                        string imgPath = Path.Combine(
                            outputDir,
                            $"page{pageNum}_img{imgIndex}{GetExtension(img)}");

                        // Save the image to disk – XImage.Save expects a Stream
                        using (FileStream fs = new FileStream(imgPath, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(fs);
                        }

                        Console.WriteLine($"Saved image: {imgPath}");
                        imgIndex++;
                    }
                }
            }

            Console.WriteLine("Image extraction completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper to determine a suitable file extension based on the image format
    // XImage in Aspose.Pdf does not expose ImageInfo, so we default to PNG.
    static string GetExtension(XImage img)
    {
        // If future versions expose format information, you can extend this method.
        // For now, always return .png to guarantee a valid image file.
        return ".png";
    }
}
