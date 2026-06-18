using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // Added for FileSpecification

class PdfDiagnosticTool
{
    static void Main(string[] args)
    {
        // Input PDF path – can be passed as first argument or hard‑coded.
        string pdfPath = args.Length > 0 ? args[0] : "sample.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // ---------- Page count ----------
        int pageCount;
        using (PdfFileInfo fileInfo = new PdfFileInfo(pdfPath))
        {
            // PdfFileInfo.NumberOfPages provides the total number of pages.
            pageCount = fileInfo.NumberOfPages;
        }

        // ---------- Image count ----------
        int imageCount = 0;
        int attachmentCount = 0; // declare here for later use
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);
            // Prepare the extractor for image extraction.
            extractor.ExtractImage();

            // Iterate through all images in the document.
            while (extractor.HasNextImage())
            {
                // Retrieve the next image into a memory stream (discarded afterwards).
                using (MemoryStream imgStream = new MemoryStream())
                {
                    extractor.GetNextImage(imgStream);
                }
                imageCount++;
            }

            // ---------- Attachment count ----------
            // Prepare the extractor for attachment extraction.
            extractor.ExtractAttachment();

            // Get the list of attachment specifications.
            List<FileSpecification> attachments = extractor.GetAttachmentInfo();
            attachmentCount = attachments?.Count ?? 0;
        }

        // Output the diagnostic information.
        Console.WriteLine($"Pages      : {pageCount}");
        Console.WriteLine($"Images     : {imageCount}");
        Console.WriteLine($"Attachments: {attachmentCount}");
    }
}
