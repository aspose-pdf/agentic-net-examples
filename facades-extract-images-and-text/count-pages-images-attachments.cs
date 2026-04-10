using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // Needed for FileSpecification

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Get total number of pages using Document (PdfPageEditor has no GetPagesCount())
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Get total number of images and attachments
        int imageCount = 0;
        int attachmentCount = 0;

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPath);

            // Count images
            extractor.ExtractImage();
            while (extractor.HasNextImage())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    extractor.GetNextImage(ms);
                }
                imageCount++;
            }

            // Count attachments
            extractor.ExtractAttachment();
            List<FileSpecification> attachments = extractor.GetAttachmentInfo();
            attachmentCount = attachments?.Count ?? 0;
        }

        Console.WriteLine($"Pages: {pageCount}");
        Console.WriteLine($"Images: {imageCount}");
        Console.WriteLine($"Attachments: {attachmentCount}");
    }
}
