using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Delete all attachments from the PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);
        editor.DeleteAttachments();
        editor.Save(outputPath);
        editor.Close();

        // Verify that no attachments remain
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(outputPath);
        // Extraction populates internal attachment list
        extractor.ExtractAttachment();
        List<FileSpecification> attachmentList = extractor.GetAttachmentInfo();
        int attachmentCount = attachmentList != null ? attachmentList.Count : 0;
        Console.WriteLine($"Attachment count after deletion: {attachmentCount}");
    }
}