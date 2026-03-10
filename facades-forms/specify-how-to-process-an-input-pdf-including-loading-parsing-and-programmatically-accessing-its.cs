using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // needed for Document and Page types

class PdfProcessingExample
{
    static void Main()
    {
        const string inputPdfPath = "sample.pdf";
        const string extractedTextPath = "extracted_text.txt";
        const string imagesOutputFolder = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Retrieve basic document information using PdfFileInfo facade.
        // -----------------------------------------------------------------
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputPdfPath))
        {
            Console.WriteLine("=== PDF Metadata ===");
            Console.WriteLine($"Title   : {fileInfo.Title}");
            Console.WriteLine($"Author  : {fileInfo.Author}");
            Console.WriteLine($"Subject : {fileInfo.Subject}");
            Console.WriteLine($"Keywords: {fileInfo.Keywords}");
            Console.WriteLine($"Pages   : {fileInfo.NumberOfPages}");
            Console.WriteLine($"Encrypted: {fileInfo.IsEncrypted}");
        }

        // ---------------------------------------------------------------
        // 2. Extract text and images using PdfExtractor facade.
        // ---------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor.
            extractor.BindPdf(inputPdfPath);

            // Set the page range to process (1‑based indexing).
            extractor.StartPage = 1;
            extractor.EndPage = extractor.Document.Pages.Count; // whole document

            // -----------------------------------------------------------------
            // Extract text.
            // -----------------------------------------------------------------
            // In recent Aspose.Pdf versions the ExtractTextMode enum has been moved to the root namespace.
            // If the enum is unavailable, the default extraction mode (TextAndFormatting) is used.
            // Uncomment the line below if your version defines Aspose.Pdf.ExtractTextMode.
            // extractor.ExtractTextMode = Aspose.Pdf.ExtractTextMode.TextAndFormatting;
            extractor.ExtractText(); // performs the extraction

            // Save extracted text to a file.
            extractor.GetText(extractedTextPath);
            Console.WriteLine($"Text extracted to: {extractedTextPath}");

            // -----------------------------------------------------------------
            // Extract images.
            // -----------------------------------------------------------------
            // Ensure the output folder exists.
            Directory.CreateDirectory(imagesOutputFolder);

            // In current Aspose.Pdf versions the default mode extracts all images, so no explicit assignment is required.
            extractor.ExtractImage(); // performs the extraction

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each image as PNG (you can change the format if needed).
                string imagePath = Path.Combine(imagesOutputFolder, $"image_{imageIndex}.png");
                extractor.GetNextImage(imagePath);
                Console.WriteLine($"Image saved: {imagePath}");
                imageIndex++;
            }

            // -----------------------------------------------------------------
            // Example of accessing page‑level information via the bound Document.
            // -----------------------------------------------------------------
            Console.WriteLine("\n=== Page Information ===");
            for (int pageNum = 1; pageNum <= extractor.Document.Pages.Count; pageNum++) // 1‑based
            {
                Page page = extractor.Document.Pages[pageNum];
                Console.WriteLine($"Page {pageNum}: Width={page.PageInfo.Width}, Height={page.PageInfo.Height}");
            }
        }

        Console.WriteLine("\nProcessing completed.");
    }
}
