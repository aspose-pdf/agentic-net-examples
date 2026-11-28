using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor (IDisposable) inside a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdf);

            // Set page range 15‑20 (1‑based indexing)
            extractor.StartPage = 15;
            extractor.EndPage   = 20;

            // The ExtractImageMode property is not available in the referenced
            // Aspose.Pdf.Facades version, so we rely on the default extraction
            // behavior (all images on the selected pages).

            // Perform the extraction
            extractor.ExtractImage();

            int imageIndex = 1;

            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                // Get the next image into a memory stream (no overload without args)
                using (MemoryStream imgStream = new MemoryStream())
                {
                    extractor.GetNextImage(imgStream);
                    string outPath = Path.Combine(outputFolder, $"image_{imageIndex}.png");
                    File.WriteAllBytes(outPath, imgStream.ToArray());
                    Console.WriteLine($"Saved: {outPath}");
                }
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
