using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        // Get total number of pages using PdfPageEditor
        int pageCount;
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(inputPdf);
            pageCount = pageEditor.GetPages();
        }

        // Get total number of images and attachments using PdfExtractor
        int imageCount = 0;
        int attachmentCount = 0;
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);

            // Extract images
            extractor.ExtractImage();
            while (extractor.HasNextImage())
            {
                using (MemoryStream imgStream = new MemoryStream())
                {
                    extractor.GetNextImage(imgStream);
                }
                imageCount++;
            }

            // Extract attachments
            extractor.ExtractAttachment();
            // GetAttachmentInfo returns a list of FileSpecification objects.
            // We only need the count, so we can query it directly without referencing the type name.
            attachmentCount = extractor.GetAttachmentInfo()?.Count ?? 0;
        }

        // Report results
        Console.WriteLine($"Pages: {pageCount}");
        Console.WriteLine($"Images: {imageCount}");
        Console.WriteLine($"Attachments: {attachmentCount}");
    }
}
