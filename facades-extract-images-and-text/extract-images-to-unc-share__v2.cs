using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // NOTE: Include the string[] args parameter so the compiler can locate the entry point.
    public static void Main(string[] args)
    {
        // UNC path to the source PDF
        const string inputPdf = @"\\server\share\input\sample.pdf";

        // UNC folder where extracted images will be saved
        const string outputFolder = @"\\server\share\output\images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor to extract images
        // The null‑forgiving operator (!) silences the nullable warning for the disposable instance.
        using (PdfExtractor extractor = new PdfExtractor()!)
        {
            // Bind the PDF file
            extractor.BindPdf(inputPdf);

            // Prepare for image extraction
            extractor.ExtractImage();

            int imageIndex = 1;
            // Loop through all images in the PDF
            while (extractor.HasNextImage())
            {
                // Build the output file name (e.g., image-1.png, image-2.png, ...)
                string outputFile = Path.Combine(outputFolder, $"image-{imageIndex}.png");

                // Extract the current image. Use the overload that does not require ImageFormat
                // (the image will be saved in its original format; if conversion is needed,
                //  post‑process the file with a separate image library).
                extractor.GetNextImage(outputFile);

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
