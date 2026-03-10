using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists for images
        Directory.CreateDirectory(outputDir);

        // -----------------------------------------------------------------
        // 1. Access basic document information using PdfFileInfo (Facade)
        // -----------------------------------------------------------------
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputPdf))
        {
            Console.WriteLine($"Title   : {fileInfo.Title}");
            Console.WriteLine($"Author  : {fileInfo.Author}");
            Console.WriteLine($"Subject : {fileInfo.Subject}");
            Console.WriteLine($"Pages   : {fileInfo.NumberOfPages}");
            Console.WriteLine($"Producer: {fileInfo.Producer}");
            Console.WriteLine($"IsEncrypted: {fileInfo.IsEncrypted}");
        }

        // ---------------------------------------------------------------
        // 2. Extract text and images using PdfExtractor (Facade)
        // ---------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // ---------- Text extraction ----------
            extractor.ExtractText();
            using (MemoryStream textStream = new MemoryStream())
            {
                // Get extracted text into a memory stream
                extractor.GetText(textStream);
                string extractedText = System.Text.Encoding.UTF8.GetString(textStream.ToArray());
                Console.WriteLine($"Extracted text length: {extractedText.Length}");
                // (Optional) write text to a file
                File.WriteAllText(Path.Combine(outputDir, "extracted_text.txt"), extractedText);
            }

            // ---------- Image extraction ----------
            extractor.ExtractImage();
            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each image directly to a file in PNG format
                string imagePath = Path.Combine(outputDir, $"image_{imageIndex}.png");
                extractor.GetNextImage(imagePath);
                Console.WriteLine($"Saved image {imageIndex} to {imagePath}");
                imageIndex++;
            }
        }

        // ---------------------------------------------------------------
        // 3. Access page‑specific information via PdfFileInfo methods
        // ---------------------------------------------------------------
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputPdf))
        {
            for (int pageNum = 1; pageNum <= fileInfo.NumberOfPages; pageNum++)
            {
                double width  = fileInfo.GetPageWidth(pageNum);
                double height = fileInfo.GetPageHeight(pageNum);
                int rotation = fileInfo.GetPageRotation(pageNum);
                Console.WriteLine($"Page {pageNum}: Width={width}, Height={height}, Rotation={rotation}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}