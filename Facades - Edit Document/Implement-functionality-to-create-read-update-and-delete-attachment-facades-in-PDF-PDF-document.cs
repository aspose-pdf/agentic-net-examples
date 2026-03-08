using System;
using System.IO;
using Aspose.Pdf.Facades;

class AttachmentCrudDemo
{
    static void Main()
    {
        // Input PDF and files to be attached
        const string inputPdfPath = "sample.pdf";
        const string attachmentFilePath = "attachment.txt";
        const string attachmentDescription = "Sample attachment";

        // Verify required files exist
        if (!File.Exists(inputPdfPath) || !File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine("Input PDF or attachment file not found.");
            return;
        }

        // -----------------------------------------------------------------
        // CREATE: add a document attachment (no annotation) and a file
        // attachment annotation on page 1.
        // -----------------------------------------------------------------
        const string createdPdfPath = "sample_with_attachments.pdf";

        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPdfPath);

            // Add attachment without any visual annotation
            editor.AddDocumentAttachment(attachmentFilePath, attachmentDescription);

            // Add a visual file‑attachment annotation.
            // CreateFileAttachment expects System.Drawing.Rectangle.
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 20, 20);
            editor.CreateFileAttachment(
                rect,                                 // annotation rectangle
                "Attachment annotation",              // contents displayed on hover
                attachmentFilePath,                   // file to attach
                1,                                    // page number (1‑based)
                "Graph");                             // icon name (Graph, PushPin, Paperclip, Tag)

            // Save the modified PDF
            editor.Save(createdPdfPath);
        }

        // -----------------------------------------------------------------
        // READ: extract a specific attachment by name using PdfExtractor.
        // -----------------------------------------------------------------
        const string extractedAttachmentPath = "extracted_attachment.txt";

        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF that contains the attachment
            extractor.BindPdf(createdPdfPath);

            // Extract the attachment with the given file name.
            // The method writes the file to the current working directory.
            extractor.ExtractAttachment(Path.GetFileName(attachmentFilePath));

            // Optionally move/rename the extracted file
            if (File.Exists(Path.GetFileName(attachmentFilePath)))
            {
                File.Move(Path.GetFileName(attachmentFilePath), extractedAttachmentPath, overwrite:true);
            }
        }

        // -----------------------------------------------------------------
        // UPDATE: replace the existing attachment with a new one.
        // -----------------------------------------------------------------
        const string newAttachmentPath = "new_attachment.txt";
        File.WriteAllText(newAttachmentPath, "Content of the updated attachment.");

        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(createdPdfPath);

            // Remove all existing attachments
            editor.DeleteAttachments();

            // Add the new attachment
            editor.AddDocumentAttachment(newAttachmentPath, "Updated attachment");

            // Save changes back to the same file
            editor.Save(createdPdfPath);
        }

        // -----------------------------------------------------------------
        // DELETE: remove all attachments from the PDF.
        // -----------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(createdPdfPath);

            // Delete every attachment in the document
            editor.DeleteAttachments();

            // Persist the removal
            editor.Save(createdPdfPath);
        }

        Console.WriteLine("Attachment CRUD operations completed successfully.");
    }
}