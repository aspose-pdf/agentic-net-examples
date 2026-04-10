using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";               // source PDF
        const string attachmentPath = "attachment_file.pdf";   // file to attach
        const string attachmentDesc = "Sample attachment";
        const string outputPdfPath = "output_with_attachment.pdf";

        // ------------------------------------------------------------
        // Ensure the source PDF exists (create a simple one if missing)
        // ------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            Document srcDoc = new Document();
            Page srcPage = srcDoc.Pages.Add();
            srcPage.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("This is a sample source PDF."));
            srcDoc.Save(inputPdfPath);
        }

        // ------------------------------------------------------------
        // Ensure the attachment file exists (create a simple PDF as attachment)
        // ------------------------------------------------------------
        if (!File.Exists(attachmentPath))
        {
            Document attDoc = new Document();
            Page attPage = attDoc.Pages.Add();
            attPage.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("This PDF is used as an attachment."));
            attDoc.Save(attachmentPath);
        }

        // ---------- Add attachment ----------
        // Create the editor (lifecycle: create)
        PdfContentEditor editor = new PdfContentEditor();

        // Load the PDF (lifecycle: load)
        editor.BindPdf(inputPdfPath);

        // Add the attachment (no annotation)
        editor.AddDocumentAttachment(attachmentPath, attachmentDesc);

        // Save the modified PDF (lifecycle: save)
        editor.Save(outputPdfPath);

        // ---------- List attachment names ----------
        // Create the extractor (lifecycle: create)
        PdfExtractor extractor = new PdfExtractor();

        // Load the PDF that now contains the attachment
        extractor.BindPdf(outputPdfPath);

        // Extract attachments first (required before GetAttachNames)
        extractor.ExtractAttachment();

        // Retrieve the list of attachment names
        IList<string> attachmentNames = extractor.GetAttachNames();

        // Output the names to confirm successful addition
        Console.WriteLine("Attachments in the PDF:");
        foreach (string name in attachmentNames)
        {
            Console.WriteLine($"- {name}");
        }
    }
}
