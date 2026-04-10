using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_attachments.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Delete all attachments using PdfContentEditor
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);
        editor.DeleteAttachments();
        editor.Save(outputPath);

        // Verify that the attachment count is zero using PdfExtractor
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(outputPath);
        extractor.ExtractAttachment();                     // required before retrieving info
        List<FileSpecification> attachmentInfo = extractor.GetAttachmentInfo();
        int attachmentCount = attachmentInfo?.Count ?? 0;

        Console.WriteLine($"Attachment count after deletion: {attachmentCount}");
    }
}