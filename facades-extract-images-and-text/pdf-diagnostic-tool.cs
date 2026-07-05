using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // Added for FileSpecification

class PdfDiagnosticTool
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Get total number of pages using PdfFileInfo
        int pageCount;
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputPdf))
        {
            pageCount = fileInfo.NumberOfPages;
        }

        // Get total number of images using PdfExtractor
        int imageCount = 0;
        using (PdfExtractor imageExtractor = new PdfExtractor())
        {
            imageExtractor.BindPdf(inputPdf);
            imageExtractor.ExtractImage();

            while (imageExtractor.HasNextImage())
            {
                // Retrieve each image into a memory stream (discarded) just to advance the iterator
                using (MemoryStream imgStream = new MemoryStream())
                {
                    imageExtractor.GetNextImage(imgStream);
                }
                imageCount++;
            }
        }

        // Get total number of attachments using PdfExtractor
        int attachmentCount = 0;
        using (PdfExtractor attachmentExtractor = new PdfExtractor())
        {
            attachmentExtractor.BindPdf(inputPdf);
            attachmentExtractor.ExtractAttachment();

            // GetAttachmentInfo returns a list of Aspose.Pdf.FileSpecification objects
            List<FileSpecification> attachments = attachmentExtractor.GetAttachmentInfo();
            if (attachments != null)
                attachmentCount = attachments.Count;
        }

        // Report results
        Console.WriteLine($"Pages      : {pageCount}");
        Console.WriteLine($"Images     : {imageCount}");
        Console.WriteLine($"Attachments: {attachmentCount}");
    }
}
