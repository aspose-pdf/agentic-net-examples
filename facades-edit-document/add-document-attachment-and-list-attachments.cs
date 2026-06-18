using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath      = "input.pdf";
        const string attachmentPath    = "attachment_file.pdf";
        const string outputPdfPath     = "output_with_attachment.pdf";

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

        // ---------- Add attachment ----------
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdfPath);
        // Add the file as a document attachment (no visual annotation)
        editor.AddDocumentAttachment(attachmentPath, "Sample attachment description");
        // Save the modified PDF
        editor.Save(outputPdfPath);
        Console.WriteLine($"Attachment added and PDF saved to '{outputPdfPath}'.");

        // ---------- List all attachment names ----------
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(outputPdfPath);
        // Must extract attachments before retrieving their names
        extractor.ExtractAttachment();
        IList<string> attachmentNames = extractor.GetAttachNames();

        Console.WriteLine("Attachments in the PDF:");
        foreach (string name in attachmentNames)
        {
            Console.WriteLine($"- {name}");
        }
    }
}