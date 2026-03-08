using System;
using System.IO;
using Aspose.Pdf.Facades;

class PdfContentExtractor
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Output folders for extracted text and images
        const string textOutputDir = "ExtractedText";
        const string imageOutputDir = "ExtractedImages";

        // Ensure output directories exist
        Directory.CreateDirectory(textOutputDir);
        Directory.CreateDirectory(imageOutputDir);

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfExtractor (a Facade) to extract text and images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document
            extractor.BindPdf(inputPdf);

            // ----- Text extraction -----
            // In recent Aspose.Pdf versions the ExtractTextMode enum was removed.
            // Calling ExtractText() starts the extraction process.
            extractor.ExtractText();

            // Save the whole document text to a single file
            string fullTextPath = Path.Combine(textOutputDir, "FullDocument.txt");
            extractor.GetText(fullTextPath);

            // Extract each page's text into separate files
            int pageIndex = 1;
            while (extractor.HasNextPageText())
            {
                string pageTextPath = Path.Combine(textOutputDir, $"Page_{pageIndex}.txt");
                extractor.GetNextPageText(pageTextPath);
                pageIndex++;
            }

            // ----- Image extraction -----
            // The ExtractImageMode enum was also removed. Calling ExtractImage()
            // initiates image extraction.
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(imageOutputDir, $"Image_{imageIndex}.png");
                // The format is inferred from the file extension.
                extractor.GetNextImage(imagePath);
                imageIndex++;
            }
        }

        Console.WriteLine("Extraction completed.");
    }
}
