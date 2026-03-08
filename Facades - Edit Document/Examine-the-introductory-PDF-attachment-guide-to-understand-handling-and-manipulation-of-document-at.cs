using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfAttachmentDemo
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf = "sample.pdf";
        const string attachmentToAdd = "newAttachment.txt";
        const string outputPdfWithAttachment = "sample_with_attachment.pdf";
        const string outputPdfWithoutAttachments = "sample_no_attachments.pdf";
        const string extractFolder = "ExtractedAttachments";

        // -------------------------------------------------
        // 1. Extract existing attachments from a PDF file
        // -------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the extraction folder exists
        Directory.CreateDirectory(extractFolder);

        // Use PdfExtractor to bind the PDF and extract attachments
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPdf);
        extractor.ExtractAttachment();                     // extracts all attachments (makes them available)
        IList<string> attachmentNames = extractor.GetAttachNames(); // list of attachment file names

        // Retrieve attachment streams
        MemoryStream[] attachmentStreams = extractor.GetAttachment();

        for (int i = 0; i < attachmentStreams.Length; i++)
        {
            string name = attachmentNames[i];
            string outPath = Path.Combine(extractFolder, name);

            // Write each attachment to disk
            using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
            {
                attachmentStreams[i].Position = 0;
                attachmentStreams[i].CopyTo(fs);
            }

            Console.WriteLine($"Extracted attachment: {outPath}");
        }

        // -------------------------------------------------
        // 2. Add a new attachment to the PDF (no annotation)
        // -------------------------------------------------
        if (!File.Exists(attachmentToAdd))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentToAdd}");
            return;
        }

        PdfContentEditor editorAdd = new PdfContentEditor();
        editorAdd.BindPdf(inputPdf);
        editorAdd.AddDocumentAttachment(attachmentToAdd, "Added via PdfContentEditor");
        editorAdd.Save(outputPdfWithAttachment);
        Console.WriteLine($"Added attachment and saved as: {outputPdfWithAttachment}");

        // -------------------------------------------------
        // 3. Delete all attachments from the PDF
        // -------------------------------------------------
        PdfContentEditor editorDelete = new PdfContentEditor();
        editorDelete.BindPdf(outputPdfWithAttachment);
        editorDelete.DeleteAttachments(); // removes every attachment
        editorDelete.Save(outputPdfWithoutAttachments);
        Console.WriteLine($"All attachments removed. Saved as: {outputPdfWithoutAttachments}");
    }
}