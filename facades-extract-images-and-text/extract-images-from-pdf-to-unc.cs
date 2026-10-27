using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        // UNC path to the network‑mapped drive
        const string outputFolder = @"\\server\share\images";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Ensure the target directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // PdfExtractor implements IDisposable – wrap in using
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Load the PDF document
                extractor.BindPdf(pdfPath);
                // Extract images from all pages
                extractor.ExtractImage();

                int imageIndex = 1;
                // Iterate over extracted images
                while (extractor.HasNextImage())
                {
                    // Get the image into a MemoryStream (the overload requires a stream)
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        extractor.GetNextImage(imgStream);
                        imgStream.Position = 0; // reset for reading

                        using (Image img = Image.FromStream(imgStream))
                        {
                            // Determine appropriate file extension
                            string ext = GetImageExtension(img);
                            string fileName = $"Image_{imageIndex}{ext}";
                            string outputPath = Path.Combine(outputFolder, fileName);

                            // Save the image to the UNC location
                            img.Save(outputPath);
                            Console.WriteLine($"Saved: {outputPath}");

                            imageIndex++;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper to map Image.RawFormat to a file extension
    static string GetImageExtension(Image img)
    {
        if (img.RawFormat.Equals(ImageFormat.Jpeg)) return ".jpg";
        if (img.RawFormat.Equals(ImageFormat.Png))  return ".png";
        if (img.RawFormat.Equals(ImageFormat.Gif))  return ".gif";
        if (img.RawFormat.Equals(ImageFormat.Bmp))  return ".bmp";
        if (img.RawFormat.Equals(ImageFormat.Tiff)) return ".tiff";
        // Fallback if format is unknown
        return ".img";
    }
}
