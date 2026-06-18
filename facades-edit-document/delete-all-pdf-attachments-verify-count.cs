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
        const string outputPath = "output_no_attachments.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Delete all attachments from the PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);          // Load the PDF
            editor.DeleteAttachments();         // Remove all attachments
            editor.Save(outputPath);            // Save the modified PDF
        }

        // Verify that the attachment count is now zero
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(outputPath);      // Load the modified PDF
            extractor.ExtractAttachment();      // Required before querying attachment info
            List<FileSpecification> attachments = extractor.GetAttachmentInfo();
            int attachmentCount = attachments?.Count ?? 0;
            Console.WriteLine($"Attachment count after deletion: {attachmentCount}");
        }
    }
}