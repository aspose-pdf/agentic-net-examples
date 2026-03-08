using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class PdfExtractionDemo
{
    static void Main()
    {
        // Input PDF file (relative to the executable's working directory)
        const string inputPdfPath = "input.pdf";

        // Verify that the PDF file exists before attempting extraction
        if (!File.Exists(inputPdfPath))
        {
            Console.WriteLine($"Error: The file '{inputPdfPath}' was not found. Please ensure the path is correct.");
            return;
        }

        // Output paths
        const string allTextPath = "AllText.txt";
        const string pagesTextFolder = "PageTexts";
        const string imagesFolder = "ExtractedImages";

        // Ensure output directories exist
        Directory.CreateDirectory(pagesTextFolder);
        Directory.CreateDirectory(imagesFolder);

        // Use PdfExtractor (Facade) – it implements IDisposable, so wrap in using
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdfPath);

            // -------------------------------------------------
            // Extract all text from the document (Unicode encoding)
            // -------------------------------------------------
            extractor.ExtractText(Encoding.Unicode);
            extractor.GetText(allTextPath); // saves the whole document text to a single file

            // -------------------------------------------------
            // Extract each page's text into separate files
            // -------------------------------------------------
            int pageNumber = 1;
            while (extractor.HasNextPageText())
            {
                string pageTextPath = Path.Combine(pagesTextFolder, $"Page_{pageNumber}.txt");
                extractor.GetNextPageText(pageTextPath);
                pageNumber++;
            }

            // -------------------------------------------------
            // Extract all images from the document
            // -------------------------------------------------
            extractor.ExtractImage(); // prepares image extraction

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each image using its original format (extension is inferred by the library)
                string imagePath = Path.Combine(imagesFolder, $"Image_{imageIndex}.png");
                extractor.GetNextImage(imagePath);
                imageIndex++;
            }
        }

        Console.WriteLine("Extraction completed.");
    }
}
