using System;
using System.IO;
using Aspose.Pdf.Facades;

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
            // PdfExtractor implements IDisposable, so we use a using block.
            using (var extractor = new PdfExtractor())
            {
                // Bind the source PDF.
                extractor.BindPdf(inputPdf);

                // Define the page range to process (1‑based indexing).
                extractor.StartPage = 5;
                extractor.EndPage   = 7;

                // The ExtractImageMode property is not available in the current
                // Aspose.Pdf version, so we rely on the default extraction behaviour.

                // Perform the image extraction.
                extractor.ExtractImage();

                // Iterate through the extracted images. In this library version the
                // ImageCount property and SaveImage method are not present. Instead we
                // use HasNextImage() / GetNextImage(Stream) to retrieve each image.
                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    string outPath = Path.Combine(outputDir, $"Image_{imageIndex}.png");
                    using (var ms = new MemoryStream())
                    {
                        // Write the current image to the memory stream.
                        extractor.GetNextImage(ms);
                        // Persist the image to disk.
                        File.WriteAllBytes(outPath, ms.ToArray());
                    }
                    Console.WriteLine($"Saved image {imageIndex} → {outPath}");
                    imageIndex++;
                }
            }

            Console.WriteLine("Image extraction completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
