using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";
        const string attachmentPath    = "attachment_file.pdf";
        const string outputPdfPath     = "output_with_attachment.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Load the source PDF, add the attachment, and save the result.
        using (Document doc = new Document(inputPdfPath))
        {
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc); // bind the loaded document
            editor.AddDocumentAttachment(attachmentPath, "Audit attachment");
            editor.Save(outputPdfPath); // save the modified PDF
        }

        // Set the document's modification date to the current UTC timestamp.
        // PdfFileInfo.ModDate expects a PDF date string, e.g., "D:20231115123045Z".
        PdfFileInfo fileInfo = new PdfFileInfo(outputPdfPath);
        string utcPdfDate = "D:" + DateTime.UtcNow.ToString("yyyyMMddHHmmss") + "Z";
        fileInfo.ModDate = utcPdfDate;

        Console.WriteLine($"Attachment added and ModDate set to UTC ({utcPdfDate}) in '{outputPdfPath}'.");
    }
}