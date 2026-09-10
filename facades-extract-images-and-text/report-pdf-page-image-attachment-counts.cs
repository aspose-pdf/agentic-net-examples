using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // Added to resolve FileSpecification

class Program
{
    static void Main(string[] args)
    {
        // Input PDF path – either from command line or a default file name
        string inputPath = args.Length > 0 ? args[0] : "sample.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // ---------- Page count ----------
        int pageCount;
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputPath))
        {
            pageCount = fileInfo.NumberOfPages;
        }

        // ---------- Images and Attachments ----------
        int imageCount = 0;
        int attachmentCount = 0;

        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPath);

            // ----- Images -----
            extractor.ExtractImage(); // Prepare image extraction
            while (extractor.HasNextImage())
            {
                // Retrieve each image into a dummy stream; we only need the count
                using (MemoryStream dummyStream = new MemoryStream())
                {
                    extractor.GetNextImage(dummyStream);
                }
                imageCount++;
            }

            // ----- Attachments -----
            extractor.ExtractAttachment(); // Prepare attachment extraction
            List<FileSpecification> attachments = extractor.GetAttachmentInfo();
            attachmentCount = attachments?.Count ?? 0;
        }

        // ---------- Report ----------
        Console.WriteLine($"Pages: {pageCount}");
        Console.WriteLine($"Images: {imageCount}");
        Console.WriteLine($"Attachments: {attachmentCount}");
    }
}
