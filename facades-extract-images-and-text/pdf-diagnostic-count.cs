using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Count pages
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Count images
        int imageCount = 0;
        using (Document doc = new Document(inputPath))
        {
            foreach (Page page in doc.Pages)
            {
                foreach (XImage img in page.Resources.Images)
                {
                    imageCount++;
                }
            }
        }

        // Count attachments
        int attachmentCount = 0;
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPath);
            // Extract attachments to populate internal collection
            extractor.ExtractAttachment();
            var attachInfo = extractor.GetAttachmentInfo(); // Use var to avoid direct dependency on AttachmentInfo type
            if (attachInfo != null)
            {
                attachmentCount = attachInfo.Count;
            }
        }

        Console.WriteLine($"Pages: {pageCount}");
        Console.WriteLine($"Images: {imageCount}");
        Console.WriteLine($"Attachments: {attachmentCount}");
    }
}
