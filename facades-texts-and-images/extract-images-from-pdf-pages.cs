using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create a temporary directory for the extracted images
        string tempDir = Path.Combine(Path.GetTempPath(),
                                      "AsposeImages_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // PdfExtractor implements IDisposable, so wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF file
            extractor.BindPdf(inputPdf);

            // Define the page range (inclusive) – pages 2 through 5
            extractor.StartPage = 2;
            extractor.EndPage   = 5;

            // The ExtractImageMode property is not available in the referenced
            // Aspose.Pdf.Facades version. The default behavior extracts all images,
            // so we simply call ExtractImage().
            // extractor.ExtractImageMode = ExtractImageMode.DefinedInResources; // removed for compatibility

            // Enable image extraction
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate over all extracted images
            while (extractor.HasNextImage())
            {
                string outPath = Path.Combine(tempDir, $"image_{imageIndex}.png");
                // GetNextImage writes the image directly to the specified file
                extractor.GetNextImage(outPath);
                imageIndex++;
            }
        }

        Console.WriteLine($"Images extracted to temporary folder: {tempDir}");
    }
}
