using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // PdfExtractor implements IDisposable – wrap it in a using block
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Load the PDF document
                extractor.BindPdf(pdfPath);

                // The ExtractImageMode property does not exist in the referenced
                // Aspose.Pdf.Facades version. Image extraction is performed by
                // calling ExtractImage() which extracts all images by default.
                extractor.ExtractImage();

                // Iterate through all extracted images
                while (extractor.HasNextImage())
                {
                    // Generate a unique file name using a GUID
                    string guidFileName = Guid.NewGuid().ToString() + ".png";
                    string outPath = Path.Combine(outputDir, guidFileName);

                    // Write the image to disk (specify PNG format)
                    extractor.GetNextImage(outPath, ImageFormat.Png);
                }
            }

            Console.WriteLine($"Images extracted to folder: {outputDir}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
