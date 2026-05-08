using System;
using System.IO;
using Aspose.Pdf;
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

        // Get total number of pages using Document.Pages.Count (PdfPageEditor.GetPages() does not return a count)
        int pageCount;
        using (Document doc = new Document(inputPdf))
        {
            pageCount = doc.Pages.Count;
        }
        Console.WriteLine($"Pages: {pageCount}");

        // Get total number of images using PdfExtractor
        int imageCount = 0;
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractImage();

            while (extractor.HasNextImage())
            {
                // Retrieve each image into a dummy stream; we only need the count
                using (MemoryStream ms = new MemoryStream())
                {
                    extractor.GetNextImage(ms);
                }
                imageCount++;
            }
        }
        Console.WriteLine($"Images: {imageCount}");

        // Get total number of attachments using PdfExtractor
        int attachmentCount = 0;
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractAttachment();

            var attachmentInfo = extractor.GetAttachmentInfo();
            attachmentCount = attachmentInfo?.Count ?? 0;
        }
        Console.WriteLine($"Attachments: {attachmentCount}");
    }
}