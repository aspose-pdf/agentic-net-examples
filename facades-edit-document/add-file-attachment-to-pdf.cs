using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // existing PDF
        const string attachmentPath = "attachment.docx"; // file to attach
        const string attachmentDesc = "Attached document"; // description
        const string outputPdfPath = "output.pdf";        // result PDF

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

        // Use PdfContentEditor facade to edit the PDF and add a hidden attachment.
        using (var editor = new PdfContentEditor())
        {
            // Bind the existing PDF file.
            editor.BindPdf(inputPdfPath);

            // Add the file attachment (no visible annotation).
            // AddDocumentAttachment is the correct method for this purpose.
            editor.AddDocumentAttachment(attachmentPath, attachmentDesc);

            // Save the modified PDF.  Use Save(...) – the older OutputFile property is obsolete.
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added. Saved to '{outputPdfPath}'.");
    }
}
