using System;
using System.Drawing; // required for System.Drawing.Rectangle used by PdfContentEditor
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // existing PDF
        const string outputPdfPath  = "output.pdf";         // result PDF
        const string attachmentPath = "Invoice2023.pdf";    // file to attach
        const string attachmentDesc = "Invoice2023";

        // Ensure the source PDF and attachment file exist before proceeding.
        if (!System.IO.File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }
        if (!System.IO.File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Create a PdfContentEditor instance and bind the existing PDF.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdfPath);

        // Create a file‑attachment annotation on page 1.
        // PdfContentEditor.CreateFileAttachment expects a System.Drawing.Rectangle.
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 20, 20);
        editor.CreateFileAttachment(rect, attachmentDesc, attachmentPath, 1, "Paperclip");

        // Retrieve the newly added annotation (last one on the page) and set its creation date.
        Page page = editor.Document.Pages[1]; // Pages are 1‑based.
        if (page.Annotations.Count > 0)
        {
            // Annotations collection is also 1‑based.
            Annotation lastAnn = page.Annotations[page.Annotations.Count];
            if (lastAnn is FileAttachmentAnnotation fileAnn)
            {
                // CreationDate expects a DateTime value.
                fileAnn.CreationDate = DateTime.Now;
            }
        }

        // Save the modified PDF.
        editor.Save(outputPdfPath);
        Console.WriteLine($"Attachment added and saved to '{outputPdfPath}'.");
    }
}
