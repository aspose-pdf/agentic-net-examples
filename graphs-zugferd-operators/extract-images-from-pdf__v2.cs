using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            using (Document pdfDoc = new Document(inputPdf))
            {
                int imageCounter = 0;

                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    Page page = pdfDoc.Pages[pageNum];

                    foreach (XImage img in page.Resources.Images)
                    {
                        imageCounter++;

                        // Save the image to a memory stream first
                        using (MemoryStream ms = new MemoryStream())
                        {
                            img.Save(ms); // XImage.Save accepts a Stream
                            ms.Position = 0;

                            // Determine the original image format from the header bytes
                            string extension = GetExtensionFromHeader(ms);

                            string fileName = $"page_{pageNum}_img_{imageCounter}{extension}";
                            // Fully qualify System.IO.Path to avoid ambiguity with Aspose.Pdf.Drawing.Path
                            string outputPath = System.IO.Path.Combine(outputDir, fileName);

                            // Write the stream to the final file
                            using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                            {
                                ms.CopyTo(fs);
                            }

                            Console.WriteLine($"Saved image: {outputPath}");
                        }
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

    // Helper that inspects the first bytes of a stream to infer the image format.
    private static string GetExtensionFromHeader(Stream stream)
    {
        // Ensure we can read the first 8 bytes without altering the original position.
        long originalPos = stream.Position;
        byte[] header = new byte[8];
        int bytesRead = stream.Read(header, 0, header.Length);
        stream.Position = originalPos; // reset for later copying

        if (bytesRead >= 2 && header[0] == 0xFF && header[1] == 0xD8)
            return ".jpg"; // JPEG
        if (bytesRead >= 8 && header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47)
            return ".png"; // PNG
        if (bytesRead >= 6 && header[0] == 0x47 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x38)
            return ".gif"; // GIF
        if (bytesRead >= 2 && header[0] == 0x42 && header[1] == 0x4D)
            return ".bmp"; // BMP
        if (bytesRead >= 4 && ((header[0] == 0x49 && header[1] == 0x49 && header[2] == 0x2A && header[3] == 0x00) ||
                               (header[0] == 0x4D && header[1] == 0x4D && header[2] == 0x00 && header[3] == 0x2A)))
            return ".tif"; // TIFF

        // Fallback to PNG – a safe default that most viewers can handle.
        return ".png";
    }
}
