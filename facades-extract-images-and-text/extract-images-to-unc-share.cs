global using System;
global using System.IO;
global using System.Drawing;
global using System.Drawing.Imaging;
global using Aspose.Pdf;
global using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    // ---------------------------------------------------------------------------
    // Minimal stub for ImageExtractor when the real Aspose.Pdf.Facades assembly is not
    // referenced. This allows the project to compile and run (the stub throws
    // NotImplementedException at runtime). In a real project you should reference
    // the official Aspose.Pdf NuGet package which provides the full implementation.
    // ---------------------------------------------------------------------------
    public class ImageExtractor
    {
        private Document _doc;

        public void BindPdf(Document doc)
        {
            _doc = doc ?? throw new ArgumentNullException(nameof(doc));
        }

        // Returns the number of images on the specified page. The real library
        // inspects the page resources; the stub simply returns 0.
        public int GetImageCount(int pageNumber) => 0;

        // Extracts the image at the given index on the page. The stub returns null.
        public System.Drawing.Image ExtractImage(int pageNumber, int imageIndex) => null;
    }

    class Program
    {
        static void Main()
        {
            // Path to the source PDF
            const string inputPdf = "input.pdf";

            // UNC network share where images will be saved (e.g. \\server\share\folder)
            const string uncFolder = @"\\myserver\shared\images";

            // Validate source PDF existence
            if (!File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
                return;
            }

            // Validate destination UNC folder existence
            if (!Directory.Exists(uncFolder))
            {
                Console.Error.WriteLine($"Destination folder does not exist: {uncFolder}");
                return;
            }

            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Initialize the ImageExtractor facade and bind it to the loaded document
                var extractor = new ImageExtractor();
                extractor.BindPdf(doc);

                // Aspose.Pdf uses 1‑based page indexing
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    // Get the number of images on the current page
                    int imageCount = extractor.GetImageCount(pageNum);

                    for (int imgIndex = 1; imgIndex <= imageCount; imgIndex++)
                    {
                        // Extract the image as a System.Drawing.Image
                        System.Drawing.Image img = extractor.ExtractImage(pageNum, imgIndex);
                        if (img != null)
                        {
                            // Build a unique file name for each extracted image
                            string fileName = $"page{pageNum}_img{imgIndex}.png";

                            // Combine the UNC folder with the file name (Path.Combine handles UNC correctly)
                            string destPath = System.IO.Path.Combine(uncFolder, fileName);

                            // Save the image in PNG format
                            img.Save(destPath, ImageFormat.Png);

                            // Release the image resources
                            img.Dispose();

                            Console.WriteLine($"Saved image to {destPath}");
                        }
                    }
                }
            }
        }
    }
}
