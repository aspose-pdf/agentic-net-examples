using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing.Imaging;          // ImageFormat for specifying output format
using Aspose.Pdf.Facades;            // PdfExtractor resides here
using System.Text.Json;               // Optional: for JSON serialization

namespace AsposePdfApi
{
    public class PdfImageExtractor
    {
        /// <summary>
        /// Extracts all images from a PDF file and returns them as Base64‑encoded strings.
        /// Each image is extracted into a MemoryStream, converted to a byte array,
        /// then encoded with Convert.ToBase64String.
        /// </summary>
        /// <param name="pdfPath">Full path to the source PDF.</param>
        /// <returns>List of Base64 strings, one per extracted image.</returns>
        public static List<string> ExtractImagesAsBase64(string pdfPath)
        {
            // Validate input
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException("PDF file not found.", pdfPath);

            var base64Images = new List<string>();

            // PdfExtractor implements IDisposable – use a using block for deterministic cleanup
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF file to the extractor
                extractor.BindPdf(pdfPath);

                // Optional: set a higher resolution if higher‑quality images are required
                // extractor.Resolution = 300;

                // Tell the extractor to look for images
                extractor.ExtractImage();

                // Iterate over all found images
                while (extractor.HasNextImage())
                {
                    // Store the current image into a memory stream (PNG format for lossless result)
                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        // GetNextImage overload with ImageFormat allows us to choose PNG
                        // The ImageFormat type is Windows‑specific; suppress the CA1416 warning for this line.
#pragma warning disable CA1416 // Validate platform compatibility
                        extractor.GetNextImage(imageStream, ImageFormat.Png);
#pragma warning restore CA1416 // Validate platform compatibility

                        // Reset stream position before reading
                        imageStream.Position = 0;

                        // Convert the stream's bytes to a Base64 string
                        string base64 = Convert.ToBase64String(imageStream.ToArray());

                        base64Images.Add(base64);
                    }
                }
            }

            return base64Images;
        }

        // Example usage: serialize the list to JSON for transmission
        public static string GetImagesJson(string pdfPath)
        {
            var imagesBase64 = ExtractImagesAsBase64(pdfPath);
            // Simple JSON array: ["data1","data2",...]
            return JsonSerializer.Serialize(imagesBase64);
        }
    }

    // Entry point required for a console application
    internal class Program
    {
        /// <summary>
        /// Main method – expects a single argument: the full path to the PDF file.
        /// Prints the JSON payload containing all extracted images as Base64 strings.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: AsposePdfApi <pdf-file-path>");
                return;
            }

            string pdfPath = args[0];
            try
            {
                string jsonPayload = PdfImageExtractor.GetImagesJson(pdfPath);
                Console.WriteLine(jsonPayload);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
