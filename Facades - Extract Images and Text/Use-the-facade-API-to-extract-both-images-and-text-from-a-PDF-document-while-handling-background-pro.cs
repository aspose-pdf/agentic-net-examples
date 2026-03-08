using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static async Task Main()
    {
        const string pdfPath = "input.pdf";
        const string textOutputPath = "extracted_text.txt";
        const string imagesOutputDir = "ExtractedImages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the images directory exists
        Directory.CreateDirectory(imagesOutputDir);

        // Run the extraction on a background thread
        await Task.Run(() => ExtractImagesAndText(pdfPath, textOutputPath, imagesOutputDir));

        Console.WriteLine("Extraction of images and text completed.");
    }

    static void ExtractImagesAndText(string pdfFile, string textFile, string imagesDir)
    {
        // PdfExtractor implements IDisposable, so wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfFile);

            // Optional: set page range (1‑based). EndPage = 0 means till the last page.
            extractor.StartPage = 1;
            extractor.EndPage = 0;

            // ----------- Extract Text -----------
            extractor.ExtractText();               // Perform the extraction
            extractor.GetText(textFile);           // Save all extracted text to a single file

            // ----------- Extract Images -----------
            extractor.ExtractImage();              // Prepare image extraction
            int imageIndex = 1;

            while (extractor.HasNextImage())
            {
                // Build a file name for each image
                string imagePath = Path.Combine(imagesDir, $"image_{imageIndex}.png");

                // Save the next image; without specifying format it keeps the original format.
                // Using .png extension works for most common image types.
                extractor.GetNextImage(imagePath);

                imageIndex++;
            }
        }
    }
}