using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath      = "input.pdf";
        const string attachmentPath    = "attachment_file.pdf";
        const string outputPdfPath     = "output_with_attachment.pdf";
        const string attachmentDesc    = "Description of the attached file";

        // Verify files exist
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

        // PdfContentEditor does not implement IDisposable, so no using block needed
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the source PDF
        editor.BindPdf(inputPdfPath);

        // Add the attachment as an embedded file (no visible annotation)
        editor.AddDocumentAttachment(attachmentPath, attachmentDesc);

        // Save the modified PDF
        editor.Save(outputPdfPath);

        Console.WriteLine($"Attachment added and saved to '{outputPdfPath}'.");
    }
}