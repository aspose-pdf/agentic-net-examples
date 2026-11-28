using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string imagesFolder = "Images";
        const string listFilePath = "ImageList.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(imagesFolder);

        // Use PdfExtractor to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdfPath);

            // The ExtractImageMode property does not exist in the current Aspose.Pdf.Facades version.
            // Calling ExtractImage() extracts all images by default.
            extractor.ExtractImage();

            // Open the list file for writing
            using (StreamWriter listWriter = new StreamWriter(listFilePath, false))
            {
                int imageIndex = 1;
                // Iterate through all extracted images
                while (extractor.HasNextImage())
                {
                    // Build a file name – we store everything as PNG for simplicity
                    string imageFileName = $"image_{imageIndex}.png";
                    string imagePath = Path.Combine(imagesFolder, imageFileName);

                    // Save the current image to disk (PNG format)
                    extractor.GetNextImage(imagePath, ImageFormat.Png);

                    // Record the file name and format in the list file
                    listWriter.WriteLine($"{imageFileName} PNG");

                    imageIndex++;
                }
            }
        }

        Console.WriteLine($"Images extracted to '{imagesFolder}'.");
        Console.WriteLine($"Image list written to '{listFilePath}'.");
    }
}
