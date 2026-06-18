using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Local PDF file to extract images from
        const string inputPdf = @"C:\Docs\sample.pdf";

        // UNC network share where images will be saved
        const string uncBase = @"\\Server\Share\Images";

        // Verify source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the UNC destination directory exists
        if (!Directory.Exists(uncBase))
        {
            Directory.CreateDirectory(uncBase);
        }

        try
        {
            // CREATE — PdfExtractor instance (disposable)
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // LOAD — bind the PDF document
                extractor.BindPdf(inputPdf);

                // EXTRACT images from the PDF
                extractor.ExtractImage();

                int imageIndex = 1;
                // Iterate through all extracted images
                while (extractor.HasNextImage())
                {
                    // Build a unique file name for each image
                    string fileName = $"image-{imageIndex}.jpg";

                    // Combine UNC base path with file name (ensures proper UNC formatting)
                    string destPath = Path.Combine(uncBase, fileName);

                    // SAVE — write the image to the UNC location
                    extractor.GetNextImage(destPath);

                    Console.WriteLine($"Saved image {imageIndex} to {destPath}");
                    imageIndex++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}