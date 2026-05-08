using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath      = "input.pdf";
        const string attachmentPath    = "attachment_file.pdf";
        const string outputPdfPath     = "output_with_attachment.pdf";

        // Verify source files exist
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
        var editor = new Aspose.Pdf.Facades.PdfContentEditor();
        editor.BindPdf(inputPdfPath);
        // Add the file as a document attachment (no visual annotation)
        editor.AddDocumentAttachment(attachmentPath, "Sample attachment added via PdfContentEditor");
        editor.Save(outputPdfPath);
        editor.Close(); // Close the facade

        Console.WriteLine($"Attachment added and PDF saved to '{outputPdfPath}'.");

        // ---------- List attachment names ----------
        var extractor = new Aspose.Pdf.Facades.PdfExtractor();
        extractor.BindPdf(outputPdfPath);
        // Must extract attachments before retrieving their names
        extractor.ExtractAttachment();
        IList<string> attachmentNames = extractor.GetAttachNames();

        Console.WriteLine("Attachments present in the PDF:");
        foreach (string name in attachmentNames)
        {
            Console.WriteLine($"- {name}");
        }

        extractor.Close(); // Close the facade
    }
}