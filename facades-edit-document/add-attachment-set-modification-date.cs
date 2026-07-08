using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentFile = "attachment.pdf";
        const string description = "Audit attachment";
        const string outputPdf = "output.pdf";

        // Verify that source files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFile}");
            return;
        }

        // Bind the PDF, add the attachment, and save the result
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);
            editor.AddDocumentAttachment(attachmentFile, description);
            editor.Save(outputPdf);
        }

        // Set the document's modification date to the current UTC timestamp
        // PDF date format: D:YYYYMMDDHHmmssZ
        PdfFileInfo fileInfo = new PdfFileInfo(outputPdf);
        string utcNow = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        fileInfo.ModDate = "D:" + utcNow + "Z";

        Console.WriteLine($"Attachment added and ModDate set. Output saved to '{outputPdf}'.");
    }
}