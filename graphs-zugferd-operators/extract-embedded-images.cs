using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a sample PDF with an embedded PNG image
        string base64Png = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XK9cAAAAASUVORK5CYII=";
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            using (MemoryStream imgStream = new MemoryStream(Convert.FromBase64String(base64Png)))
            {
                // Use the Aspose.Pdf.Rectangle (page rectangle) explicitly to avoid ambiguity
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);
                page.AddImage(imgStream, rect);
            }
            doc.Save("sample.pdf");
        }

        // Reopen the PDF and extract images preserving original format and resolution
        using (Document doc = new Document("sample.pdf"))
        {
            int extractedCount = 0;
            foreach (Page page in doc.Pages)
            {
                foreach (XImage img in page.Resources.Images)
                {
                    extractedCount++;
                    if (extractedCount > 4)
                    {
                        break;
                    }

                    // Save the XImage to a memory stream first
                    using (MemoryStream tempStream = new MemoryStream())
                    {
                        img.Save(tempStream);
                        byte[] imageBytes = tempStream.ToArray();
                        string extension = GetImageExtension(imageBytes);
                        string outputFile = "image_" + extractedCount + extension;
                        File.WriteAllBytes(outputFile, imageBytes);
                    }
                }
                if (extractedCount > 4)
                {
                    break;
                }
            }
        }
    }

    // Simple image format detection based on file header bytes
    private static string GetImageExtension(byte[] data)
    {
        if (data == null || data.Length < 4)
        {
            return ".bin";
        }
        // PNG: 89 50 4E 47
        if (data[0] == 0x89 && data[1] == 0x50 && data[2] == 0x4E && data[3] == 0x47)
        {
            return ".png";
        }
        // JPEG: FF D8 FF
        if (data[0] == 0xFF && data[1] == 0xD8 && data[2] == 0xFF)
        {
            return ".jpg";
        }
        // GIF: 47 49 46 38
        if (data[0] == 0x47 && data[1] == 0x49 && data[2] == 0x46 && data[3] == 0x38)
        {
            return ".gif";
        }
        // BMP: 42 4D
        if (data[0] == 0x42 && data[1] == 0x4D)
        {
            return ".bmp";
        }
        // TIFF (little endian): 49 49 2A 00
        if (data[0] == 0x49 && data[1] == 0x49 && data[2] == 0x2A && data[3] == 0x00)
        {
            return ".tif";
        }
        // TIFF (big endian): 4D 4D 00 2A
        if (data[0] == 0x4D && data[1] == 0x4D && data[2] == 0x00 && data[3] == 0x2A)
        {
            return ".tif";
        }
        // Default fallback
        return ".bin";
    }
}