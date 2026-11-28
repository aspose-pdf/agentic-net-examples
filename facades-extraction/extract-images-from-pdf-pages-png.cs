using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfExtractor implements IDisposable – wrap it in a using block.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF.
            extractor.BindPdf(pdfPath);

            // Define the page range to process.
            extractor.StartPage = 4;
            extractor.EndPage   = 6;

            // The ExtractImageMode property is not available in the referenced
            // Aspose.Pdf.Facades version, so we rely on the default extraction
            // behavior (all images on the selected pages).

            // Perform the extraction.
            extractor.ExtractImage();

            int imageIndex = 0;

            // Iterate through extracted images and save each as PNG.
            while (extractor.HasNextImage())
            {
                using (MemoryStream imgStream = new MemoryStream())
                {
                    // GetNextImage writes the image data to the provided stream.
                    extractor.GetNextImage(imgStream);
                    string outFile = $"image_{++imageIndex}.png";
                    File.WriteAllBytes(outFile, imgStream.ToArray());
                    Console.WriteLine($"Saved image: {outFile}");
                }
            }

            if (imageIndex == 0)
                Console.WriteLine("No images were found in the specified page range.");
        }
    }
}
