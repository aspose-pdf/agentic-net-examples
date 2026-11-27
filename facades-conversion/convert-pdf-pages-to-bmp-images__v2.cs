using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Initialize the PdfConverter
        PdfConverter converter = new PdfConverter();

        // Bind the PDF document
        converter.BindPdf(inputPdfPath);

        // Prepare for conversion (required before calling GetNextImage)
        converter.DoConvert();

        int imageIndex = 1;

        // Loop through all generated images
        while (converter.HasNextImage())
        {
            // Store the image in memory first
            using (MemoryStream imageStream = new MemoryStream())
            {
                // Get the next image as BMP
                converter.GetNextImage(imageStream, ImageFormat.Bmp);

                // Reset stream position before reading
                imageStream.Position = 0;

                // Save the BMP to a file
                string outputPath = Path.Combine(outputFolder, $"page_{imageIndex}.bmp");
                using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    imageStream.CopyTo(file);
                }

                Console.WriteLine($"Saved image {imageIndex} to '{outputPath}'.");
                imageIndex++;
            }
        }

        // Release resources if the API provides a Close method
        try
        {
            converter.Close();
        }
        catch
        {
            // Close may not exist in some versions; ignore if unavailable
        }
    }
}