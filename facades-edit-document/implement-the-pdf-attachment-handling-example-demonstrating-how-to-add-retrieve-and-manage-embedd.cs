using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfAttachmentDemo
{
    static void Main()
    {
        // Input PDF (must exist)
        const string inputPdfPath = "input.pdf";

        // Paths for intermediate and final PDFs
        const string attachedPdfPath = "output_with_attachments.pdf";
        const string cleanedPdfPath   = "output_without_attachments.pdf";

        // Directory where extracted attachments will be saved
        const string extractDir = "ExtractedAttachments";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Add attachments to the PDF using PdfContentEditor (facade)
        // ------------------------------------------------------------
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the source PDF
        editor.BindPdf(inputPdfPath);

        // a) Add a document attachment without any visual annotation
        //    (the file will be embedded in the PDF's attachment collection)
        string docAttachmentPath = "attachment_file.pdf";
        if (File.Exists(docAttachmentPath))
        {
            editor.AddDocumentAttachment(docAttachmentPath, "Sample document attachment");
        }
        else
        {
            Console.WriteLine($"Warning: attachment file not found: {docAttachmentPath}");
        }

        // b) Add a file attachment annotation on page 1
        //    This creates a visible icon that users can click to open the attachment.
        string annotationAttachmentPath = "annotation_file.pdf";
        if (File.Exists(annotationAttachmentPath))
        {
            // Rectangle defining the annotation location (System.Drawing.Rectangle)
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 600, 30, 30);

            // Create the annotation with an icon name (e.g., "Paperclip")
            editor.CreateFileAttachment(
                rect,
                "Attachment annotation",
                annotationAttachmentPath,
                1,               // page number (1‑based)
                "Paperclip");    // icon name
        }
        else
        {
            Console.WriteLine($"Warning: annotation attachment file not found: {annotationAttachmentPath}");
        }

        // Save the PDF that now contains the attachments
        editor.Save(attachedPdfPath);
        Console.WriteLine($"Attachments added. Saved to '{attachedPdfPath}'.");

        // ------------------------------------------------------------
        // 2. Extract all attachments from the PDF using PdfExtractor
        // ------------------------------------------------------------
        // Ensure the extraction directory exists
        Directory.CreateDirectory(extractDir);

        PdfExtractor extractor = new PdfExtractor();

        // Bind the PDF that contains attachments
        extractor.BindPdf(attachedPdfPath);

        // Extract attachment objects from the PDF
        extractor.ExtractAttachment();

        // Retrieve attachment names (IList<string>)
        IList<string> attachNames = extractor.GetAttachNames();

        // Retrieve attachment streams (one MemoryStream per attachment)
        MemoryStream[] attachStreams = extractor.GetAttachment();

        // Save each attachment to the extraction directory
        for (int i = 0; i < attachStreams.Length; i++)
        {
            string name = attachNames[i];
            string outPath = Path.Combine(extractDir, name);

            // Reset stream position before reading
            attachStreams[i].Position = 0;

            using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
            {
                attachStreams[i].CopyTo(fs);
            }

            Console.WriteLine($"Extracted attachment: {name} -> {outPath}");
        }

        // ------------------------------------------------------------
        // 3. Delete all attachments from the PDF (cleanup)
        // ------------------------------------------------------------
        PdfContentEditor cleaner = new PdfContentEditor();
        cleaner.BindPdf(attachedPdfPath);
        cleaner.DeleteAttachments();          // removes both document and annotation attachments
        cleaner.Save(cleanedPdfPath);
        Console.WriteLine($"All attachments removed. Clean PDF saved to '{cleanedPdfPath}'.");
    }
}
