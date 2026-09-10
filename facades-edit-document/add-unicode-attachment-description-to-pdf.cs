using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for source PDF, output PDF and the file to be attached
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_with_attachment.pdf";
        const string attachmentFilePath = "sample.txt";

        // Unicode description (Cyrillic characters and an emoji)
        const string attachmentDescription = "Описание – тест 🚀";

        // -----------------------------------------------------------------
        // Ensure the source PDF exists – create a minimal placeholder if missing
        // -----------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add(); // add a single blank page
            placeholder.Save(inputPdfPath);
        }

        // Ensure the attachment file exists; create a simple text file if missing
        if (!File.Exists(attachmentFilePath))
        {
            File.WriteAllText(attachmentFilePath, "Sample attachment content");
        }

        // ------------------------------------------------------------
        // Add the attachment to the PDF using PdfContentEditor (Facades API)
        // ------------------------------------------------------------
        using (var editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdfPath); // Load the source PDF
            editor.AddDocumentAttachment(attachmentFilePath, attachmentDescription); // Add attachment with Unicode description
            editor.Save(outputPdfPath); // Save the modified PDF
        }

        // ------------------------------------------------------------
        // Verify that the attachment description is stored correctly
        // ------------------------------------------------------------
        using (var extractor = new PdfExtractor())
        {
            extractor.BindPdf(outputPdfPath); // Load the PDF that now contains the attachment
            extractor.ExtractAttachment(); // Extract attachment information (required before GetAttachmentInfo)

            // GetAttachmentInfo returns an array of AttachmentInfo objects
            var attachmentInfos = extractor.GetAttachmentInfo();

            foreach (var info in attachmentInfos)
            {
                Console.WriteLine($"Attachment Name: {info.Name}");
                Console.WriteLine($"Attachment Description: {info.Description}");
            }
        }
    }
}
