using System;
using System.IO;
using System.Drawing.Imaging;               // ImageFormat enum
using Aspose.Pdf;                           // Document
using Aspose.Pdf.Facades;                  // PdfConverter

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output directory for BMP images
        const string outputDir = "BmpImages";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // If the input PDF does not exist, create a simple one so the sample runs without error
        if (!File.Exists(inputPdf))
        {
            using (Document sampleDoc = new Document())
            {
                // Add a blank page (or you could add some content here)
                sampleDoc.Pages.Add();
                sampleDoc.Save(inputPdf);
            }
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Initialize the PdfConverter facade with the loaded document
            using (PdfConverter converter = new PdfConverter(pdfDoc))
            {
                // The default coordinate type is CropBox, which automatically crops margins.
                // No explicit setting is required for CoordinateType.

                // Optionally, you can set a higher resolution if the library version supports it.
                // If the Resolution class is unavailable, the converter will use its default DPI.
                // Uncomment the following line if Resolution is available in your Aspose.PDF version:
                // converter.Resolution = new Resolution(300);

                // Prepare the converter for image extraction
                converter.DoConvert();

                int pageIndex = 1; // 1‑based page numbering
                // Iterate over all pages and save each as a BMP file
                while (converter.HasNextImage())
                {
                    string outputPath = Path.Combine(outputDir, $"page_{pageIndex}_out.bmp");
                    // ImageFormat.Bmp is Windows‑only; suppress the CA1416 warning for this call site.
#pragma warning disable CA1416 // Platform compatibility
                    converter.GetNextImage(outputPath, ImageFormat.Bmp);
#pragma warning restore CA1416 // Platform compatibility
                    pageIndex++;
                }
            }
        }

        Console.WriteLine("PDF has been converted to BMP images successfully.");
    }
}
