using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";      // source PDF
        const string attachmentPath    = "attachment.pdf"; // file to attach
        const string outputPdfPath     = "output.pdf";     // result PDF

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Add the attachment (no annotation) using PdfContentEditor
        // ------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPdfPath);

            // Add the attachment with a description
            editor.AddDocumentAttachment(attachmentPath, "Audit attachment");

            // Save the modified PDF
            editor.Save(outputPdfPath);
        }

        // ------------------------------------------------------------
        // 2. Set the document's modification date to current UTC
        // ------------------------------------------------------------
        // PdfFileInfo provides access to the ModDate property.
        PdfFileInfo fileInfo = new PdfFileInfo();
        fileInfo.BindPdf(outputPdfPath);

        // PDF date format: D:YYYYMMDDHHmmssZ (UTC)
        string utcDate = DateTime.UtcNow.ToString("yyyyMMddHHmmss'Z'");
        fileInfo.ModDate = "D:" + utcDate;

        // Persist the updated ModDate
        fileInfo.Save(outputPdfPath);

        Console.WriteLine($"Attachment added and ModDate set. Output saved to '{outputPdfPath}'.");
    }
}