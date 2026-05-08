using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the attachment file, and the output PDF
        const string sourcePdfPath   = "source.pdf";
        const string attachmentPath  = "attachment.txt";
        const string outputPdfPath   = "output_with_attachment.pdf";

        // Unicode description for the attachment (contains Cyrillic and an emoji)
        const string attachmentDescription = "Описание 📄 – тестовое вложение";

        // Verify that the required files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Add the attachment with Unicode description using PdfContentEditor
        // -----------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(sourcePdfPath);
            // AddDocumentAttachment(string fileAttachmentPath, string description)
            editor.AddDocumentAttachment(attachmentPath, attachmentDescription);
            editor.Save(outputPdfPath);
        }

        // ---------------------------------------------------------------
        // Extract attachment information to verify the stored Unicode text
        // ---------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(outputPdfPath);
            // Extraction must be performed before retrieving attachment info
            extractor.ExtractAttachment();

            // GetAttachmentInfo returns IList<FileSpecification>
            var attachmentInfos = extractor.GetAttachmentInfo(); // IList<FileSpecification>
            Console.WriteLine("Attachments found in the PDF:");
            foreach (var spec in attachmentInfos)
            {
                // Use Name for the original file name and Description for the Unicode text
                Console.WriteLine($"- FileName: {spec.Name}");
                Console.WriteLine($"  Description: {spec.Description}");
            }
        }
    }
}
