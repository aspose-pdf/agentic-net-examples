using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfExtractor facade to work with the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdf);

            // ---------- Detect images ----------
            bool hasImages = false;
            int imageCount = 0;

            // Prepare extractor for image extraction
            extractor.ExtractImage();

            // Iterate through all images in the document
            while (extractor.HasNextImage())
            {
                // Retrieve each image into a memory stream (no need to write to disk)
                using (MemoryStream imgStream = new MemoryStream())
                {
                    extractor.GetNextImage(imgStream);
                    imageCount++;
                }
            }

            hasImages = imageCount > 0;
            Console.WriteLine($"Images found: {hasImages} (count = {imageCount})");

            // ---------- Detect text ----------
            bool hasText = false;
            int pagesProcessed = 0;

            // Prepare extractor for text extraction
            extractor.ExtractText();

            // Iterate through each page's extracted text
            while (extractor.HasNextPageText())
            {
                using (MemoryStream txtStream = new MemoryStream())
                {
                    extractor.GetNextPageText(txtStream);
                    string pageText = Encoding.UTF8.GetString(txtStream.ToArray());

                    if (!string.IsNullOrWhiteSpace(pageText))
                    {
                        hasText = true;
                    }

                    pagesProcessed++;
                }
            }

            Console.WriteLine($"Text found: {hasText} (pages processed = {pagesProcessed})");
        }
    }
}