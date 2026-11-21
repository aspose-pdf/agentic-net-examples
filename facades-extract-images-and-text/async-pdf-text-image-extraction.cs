using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aspose.Pdf.Facades;
using Aspose.Pdf;               // Added for Document page count
using System.Drawing.Imaging;

namespace AsposePdfApi
{
    public static class PdfAsyncExtractor
    {
        // Asynchronously extracts all text from the specified PDF file.
        // Returns the extracted text as a single string.
        public static async Task<string> ExtractTextAsync(string pdfPath)
        {
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF file not found: {pdfPath}");

            // The extraction itself is CPU‑bound, so run it on a thread‑pool thread.
            return await Task.Run(() =>
            {
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(pdfPath);
                    extractor.ExtractText();

                    using (MemoryStream ms = new MemoryStream())
                    {
                        extractor.GetText(ms);
                        ms.Position = 0;
                        using (StreamReader reader = new StreamReader(ms))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }).ConfigureAwait(false);
        }

        // Asynchronously extracts all images from the specified PDF file.
        // Saves each image to the outputFolder and returns the list of saved file paths.
        public static async Task<IReadOnlyList<string>> ExtractImagesAsync(string pdfPath, string outputFolder)
        {
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF file not found: {pdfPath}");

            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            return await Task.Run(() =>
            {
                List<string> savedFiles = new List<string>();

                // Use Document to obtain the total page count (PdfExtractor has no PageCount property).
                using (Document doc = new Document(pdfPath))
                {
                    int totalPages = doc.Pages.Count;

                    for (int page = 1; page <= totalPages; page++)
                    {
                        using (PdfExtractor extractor = new PdfExtractor())
                        {
                            // Bind the PDF and limit extraction to the current page.
                            extractor.BindPdf(pdfPath);
                            extractor.StartPage = page;   // 1‑based page index
                            extractor.EndPage   = page;

                            // Ensure images defined in resources are also extracted.
                            extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

                            // Extract images from the current page.
                            extractor.ExtractImage();

                            int imageIndex = 1;
                            while (extractor.HasNextImage())
                            {
                                string fileName = Path.Combine(outputFolder, $"Page{page}_Img{imageIndex}.png");
                                // GetNextImage writes the image directly to the file using the supplied format.
                                extractor.GetNextImage(fileName, ImageFormat.Png);
                                savedFiles.Add(fileName);
                                imageIndex++;
                            }
                        }
                    }
                }

                // Return a read‑only view to satisfy the IReadOnlyList<string> contract.
                return savedFiles.AsReadOnly();
            }).ConfigureAwait(false);
        }
    }

    // Minimal entry point to satisfy the compiler. In a real library this would be omitted
    // or the project would be changed to a class‑library, but adding a stub Main resolves
    // the CS5001 error without affecting the async API.
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            // Example usage (can be removed in production).
            if (args.Length >= 2)
            {
                string pdfPath = args[0];
                string outputFolder = args[1];
                try
                {
                    string text = await PdfAsyncExtractor.ExtractTextAsync(pdfPath);
                    Console.WriteLine("Extracted text length: " + text.Length);
                    var images = await PdfAsyncExtractor.ExtractImagesAsync(pdfPath, outputFolder);
                    Console.WriteLine($"Extracted {images.Count} images to '{outputFolder}'.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Usage: AsposePdfApi <pdfPath> <outputFolder>");
            }
        }
    }
}
