using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_no_attachments.pdf";

        // Ensure the input PDF exists – create a simple PDF with a dummy attachment if needed
        if (!File.Exists(inputPdf))
        {
            CreateSamplePdfWithAttachment(inputPdf);
        }

        // Delete all attachments using PdfContentEditor
        var editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);
        editor.DeleteAttachments();
        editor.Save(outputPdf);

        // Verify that the attachment count is zero
        var extractor = new PdfExtractor();
        extractor.BindPdf(outputPdf);
        // GetAttachmentInfo returns a List<FileSpecification>
        var attachments = extractor.GetAttachmentInfo();
        int attachmentCount = attachments?.Count ?? 0;

        Console.WriteLine($"Attachment count after deletion: {attachmentCount}");
    }

    private static void CreateSamplePdfWithAttachment(string path)
    {
        // Create a one‑page PDF document
        var doc = new Document();
        doc.Pages.Add();

        // Create a dummy file in memory to embed as an attachment
        string dummyFileName = "dummy.txt";
        byte[] dummyContent = System.Text.Encoding.UTF8.GetBytes("This is a dummy attachment.");
        var fileSpec = new FileSpecification(dummyFileName, "Dummy attachment");
        fileSpec.Contents = new MemoryStream(dummyContent);

        // Add the attachment to the PDF and save it
        doc.EmbeddedFiles.Add(fileSpec);
        doc.Save(path);
    }
}
