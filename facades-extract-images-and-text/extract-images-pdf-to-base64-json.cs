using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

namespace AsposePdfImageExtraction
{
    // Model for JSON output
    public class ImageInfo
    {
        // Initialized to avoid nullable warnings
        public string Base64 { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public long FileSize { get; set; }
    }

    class Program
    {
        static void Main()
        {
            const string inputPdfPath = "input.pdf";
            const string tempImageDir = "extracted_images";

            // Validate input PDF
            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
                return;
            }

            // Ensure temporary directory exists
            Directory.CreateDirectory(tempImageDir);

            // List to hold image metadata
            List<ImageInfo> images = new List<ImageInfo>();

            // Use PdfExtractor to pull images from the PDF
            using (Aspose.Pdf.Facades.PdfExtractor extractor = new Aspose.Pdf.Facades.PdfExtractor())
            {
                extractor.BindPdf(inputPdfPath);
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    // Save each image as PNG to a temporary file
                    string tempFileName = $"image_{imageIndex}.png";
                    string tempFilePath = Path.Combine(tempImageDir, tempFileName);

                    // GetNextImage requires an ImageFormat from System.Drawing.Imaging
                    extractor.GetNextImage(tempFilePath, ImageFormat.Png);

                    // Read the saved image bytes
                    byte[] imageBytes = File.ReadAllBytes(tempFilePath);
                    string base64String = Convert.ToBase64String(imageBytes);

                    // Populate metadata object
                    ImageInfo info = new ImageInfo
                    {
                        Base64 = base64String,
                        FileName = tempFileName,
                        FileSize = imageBytes.Length
                    };

                    images.Add(info);
                    imageIndex++;
                }
            }

            // Serialize the list to JSON
            string jsonOutput = JsonSerializer.Serialize(images, new JsonSerializerOptions { WriteIndented = true });

            // Output JSON to console (or write to a file if desired)
            Console.WriteLine(jsonOutput);
        }
    }
}