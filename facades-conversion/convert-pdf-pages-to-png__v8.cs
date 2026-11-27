using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "Pages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // PdfConverter implements IDisposable, ensure proper disposal
        using (PdfConverter converter = new PdfConverter())
        {
            // Load and bind the PDF document
            converter.BindPdf(inputPath);

            // Perform the conversion to images (keeps images in memory for GetNextImage)
            converter.DoConvert();

            int pageNumber = 1;
            // Iterate through each generated image
            while (converter.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.png");

                // GetNextImage requires a destination stream (and optionally an ImageFormat).
                // We write the image directly to a MemoryStream and then persist the bytes.
                using (var ms = new MemoryStream())
                {
                    // Write the next page image as PNG into the memory stream.
                    converter.GetNextImage(ms, ImageFormat.Png);
                    // Persist the PNG data to disk.
                    File.WriteAllBytes(outPath, ms.ToArray());
                }

                Console.WriteLine($"Saved page {pageNumber} as PNG.");
                pageNumber++;
            }
        }
    }
}
