using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPdfPath = "input.pdf";

        // Ensure the input PDF exists; create a minimal PDF if it does not.
        if (!File.Exists(inputPdfPath))
        {
            using (var doc = new Document())
            {
                doc.Pages.Add(); // add a blank page
                doc.Save(inputPdfPath);
            }
        }

        // Create a unique temporary folder for extracted images
        string tempFolder = Path.Combine(Path.GetTempPath(), "PdfImages_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Initialize the PdfExtractor facade
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdfPath);

            // Extract images using the default extraction mode (DefinedInResources)
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate through all extracted images
            while (extractor.HasNextImage())
            {
                // Build the output file name (preserve original image format)
                string outputImagePath = Path.Combine(tempFolder, $"image-{imageIndex}.img");

                // Save the current image to the file system using the overload that does not require ImageFormat
                extractor.GetNextImage(outputImagePath);

                imageIndex++;
            }
        }

        Console.WriteLine($"Images extracted to temporary folder: {tempFolder}");
    }
}
