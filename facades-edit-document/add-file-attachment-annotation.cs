using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string attachmentFile = "attachment.docx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(attachmentFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFile}");
            return;
        }

        // PdfContentEditor is a facade for editing PDF content.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPdf);

            // Define the annotation rectangle (x, y, width, height) using System.Drawing.Rectangle.
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 100, 100);

            // Create a file attachment annotation on page 1.
            // Parameters: rectangle, annotation contents, file to attach, page number (1‑based), icon name.
            editor.CreateFileAttachment(rect, "Attached document", attachmentFile, 1, "Paperclip");

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"PDF with file‑attachment link saved to '{outputPdf}'.");
    }
}