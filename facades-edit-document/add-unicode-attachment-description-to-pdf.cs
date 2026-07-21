using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_attachment.pdf";
        const string attachmentPath = "sample.txt";
        // Unicode description containing Cyrillic characters and an emoji
        const string attachmentDescription = "Описание 📄 – тест";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
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
            // Bind the existing PDF document
            editor.BindPdf(inputPdf);
            // Add the attachment; the description is stored in Unicode
            editor.AddDocumentAttachment(attachmentPath, attachmentDescription);
            // Save the modified PDF
            editor.Save(outputPdf);
        }

        // ---------------------------------------------------------------
        // Verify that the Unicode description was stored correctly
        // ---------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF that now contains the attachment
            extractor.BindPdf(outputPdf);
            // Extract attachment information (required before reading it)
            extractor.ExtractAttachment();

            // Get information about all attachments
            var attachmentInfos = extractor.GetAttachmentInfo();

            foreach (var info in attachmentInfos)
            {
                // The attachment name
                Console.WriteLine($"Attachment Name: {info.Name}");
                // The Unicode description that was set earlier
                Console.WriteLine($"Description    : {info.Description}");
            }
        }
    }
}