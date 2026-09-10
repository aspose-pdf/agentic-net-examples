using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "source.pdf";
        const string outputPdfPath = "output_with_attachment.pdf";

        // ---------------------------------------------------------------------
        // 1. Create a minimal source PDF so the example can run in an empty sandbox.
        // ---------------------------------------------------------------------
        using (Document seed = new Document())
        {
            seed.Pages.Add(); // add a blank page
            seed.Save(inputPdfPath);
        }

        // ---------------------------------------------------------------------
        // 2. Prepare the attachment data in a memory stream.
        // ---------------------------------------------------------------------
        byte[] attachmentData = System.Text.Encoding.UTF8.GetBytes("This is the content of the attachment.");
        using (MemoryStream attachmentStream = new MemoryStream(attachmentData))
        {
            // Ensure the stream is positioned at the beginning before passing it to the editor.
            attachmentStream.Position = 0;

            // -----------------------------------------------------------------
            // 3. Use PdfContentEditor to bind the PDF, add the attachment, and save.
            // -----------------------------------------------------------------
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPdfPath);
            editor.AddDocumentAttachment(attachmentStream, "attachment.txt", "Sample text attachment");
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added. Output saved to '{outputPdfPath}'.");
    }
}
