using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;
using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "images.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        var images = new List<ImageInfo>();
        int index = 1;

        // Use PdfExtractor facade to pull images from the PDF
        using (var extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractImage();

            while (extractor.HasNextImage())
            {
                using (var ms = new MemoryStream())
                {
                    // Extract each image as PNG (any supported ImageFormat can be used)
                    extractor.GetNextImage(ms, ImageFormat.Png);
                    byte[] data = ms.ToArray();

                    // Base64 representation of the image bytes
                    string base64 = Convert.ToBase64String(data);

                    // Obtain basic metadata (width, height) using System.Drawing.Image
                    int width, height;
                    using (var img = Image.FromStream(new MemoryStream(data)))
                    {
                        width = img.Width;
                        height = img.Height;
                    }

                    images.Add(new ImageInfo
                    {
                        Index = index,
                        Base64 = base64,
                        Width = width,
                        Height = height,
                        Format = "png"
                    });
                }
                index++;
            }
        }

        // Serialize the collection to JSON
        string json = JsonSerializer.Serialize(images, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(outputJson, json);
        Console.WriteLine($"Extracted {images.Count} images to '{outputJson}'.");
    }

    // Simple DTO for JSON output
    private class ImageInfo
    {
        public int Index { get; set; }
        public string Base64 { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Format { get; set; }
    }
}