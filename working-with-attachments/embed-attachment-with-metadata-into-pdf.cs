using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string attachmentFilePath = "attachment.txt";
        const string outputPdfPath = "output.pdf";

        // Verify that both the source PDF and the attachment exist.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // Load the existing PDF document.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // ------------------------------------------------------------
            // Embed the attachment into the PDF using a FileSpecification.
            // ------------------------------------------------------------
            var fileSpec = new FileSpecification(attachmentFilePath, "Attachment added via Aspose.Pdf");
            // Load the file contents into a memory stream – this avoids keeping the file handle open.
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentFilePath));
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // Add custom metadata about the attachment to the document information dictionary.
            pdfDoc.Info.Add("AttachmentFileName", Path.GetFileName(attachmentFilePath));
            pdfDoc.Info.Add("AttachmentDescription", "Sample attachment added via Aspose.Pdf");

            // Configure PDF save options – the default behavior already embeds attached files.
            PdfSaveOptions saveOptions = new PdfSaveOptions();
            // No explicit EmbedAttachments property exists in the current Aspose.Pdf version.
            // The embedded files added to the document are automatically written when saving.

            // Save the modified PDF with the embedded file and metadata.
            pdfDoc.Save(outputPdfPath, saveOptions);
        }

        Console.WriteLine($"PDF saved with attachment and metadata to '{outputPdfPath}'.");
    }
}
