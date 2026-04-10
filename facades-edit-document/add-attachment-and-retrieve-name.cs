using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the file to attach, and the resulting PDF
        const string sourcePdfPath = "input.pdf";
        const string attachmentFilePath = "attachment.txt";
        const string outputPdfPath = "output_with_attachment.pdf";

        // Verify that the required files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // -------------------------------------------------
        // Add the attachment using PdfContentEditor (Facades API)
        // -------------------------------------------------
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(sourcePdfPath);
        // Add the attachment without creating a visible annotation
        editor.AddDocumentAttachment(attachmentFilePath, "Sample attachment description");
        editor.Save(outputPdfPath);

        // -------------------------------------------------
        // Retrieve and display the attachment's file specification name
        // -------------------------------------------------
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(outputPdfPath);
        // Must extract attachments before querying their names
        extractor.ExtractAttachment();
        IList<string> attachmentNames = extractor.GetAttachNames();

        Console.WriteLine("Attachments found in the PDF:");
        foreach (string name in attachmentNames)
        {
            Console.WriteLine($"- {name}");
        }
    }
}