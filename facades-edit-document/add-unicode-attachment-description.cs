using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the file to attach, and the resulting PDF
        const string sourcePdfPath      = "source.pdf";
        const string attachmentFilePath = "attachment.pdf";
        const string outputPdfPath      = "output_with_attachment.pdf";

        // Ensure the source PDF and attachment exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Add the attachment with a Unicode description using PdfContentEditor
        // -----------------------------------------------------------------
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(sourcePdfPath);

        // Unicode description containing Chinese characters and an emoji
        string unicodeDescription = "Unicode description: 測試中文字符 🚀";

        // Add the attachment (no visual annotation is created)
        editor.AddDocumentAttachment(attachmentFilePath, unicodeDescription);

        // Save the modified PDF
        editor.Save(outputPdfPath);
        Console.WriteLine($"Attachment added and PDF saved to '{outputPdfPath}'.");

        // -----------------------------------------------------------------
        // 2. Verify that the description was stored correctly
        //    using PdfExtractor to read attachment information.
        // -----------------------------------------------------------------
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(outputPdfPath);

        // Extract attachment information (includes description)
        extractor.ExtractAttachment();

        // Get the list of attachment info objects
        var attachmentInfos = extractor.GetAttachmentInfo();

        Console.WriteLine("Attachment information extracted from the PDF:");
        foreach (var info in attachmentInfos)
        {
            // The AttachmentInfo class provides Name and Description properties
            Console.WriteLine($"- Name: {info.Name}");
            Console.WriteLine($"  Description: {info.Description}");
        }

        // Clean up facades (they do not implement IDisposable, so just close)
        extractor.Close();
        editor.Close();
    }
}