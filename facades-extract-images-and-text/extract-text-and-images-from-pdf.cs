using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // <-- added for ExtractImageMode enum

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // source PDF
        const string textOutput = "extracted.txt";   // text file destination
        const string imageFolder = "Images";         // folder for extracted images

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Ensure the image output directory exists
        Directory.CreateDirectory(imageFolder);

        // PdfExtractor implements IDisposable, so use a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Enable both text and image extraction
            // Text extraction mode: 0 = pure text (default)
            extractor.ExtractTextMode = 0;

            // Image extraction mode: extract actually used images
            extractor.ExtractImageMode = Aspose.Pdf.ExtractImageMode.ActuallyUsed; // <-- corrected namespace

            // Perform the extraction operations
            extractor.ExtractText();
            extractor.ExtractImage();

            // Save the extracted text to a file
            extractor.GetText(textOutput);

            // Save each extracted image to the specified folder
            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(imageFolder, $"image-{imageIndex}.png");
                // Use the overload that infers format from the file extension
                extractor.GetNextImage(imagePath);
                imageIndex++;
            }
        }

        Console.WriteLine("Extraction of text and images completed successfully.");
    }
}
