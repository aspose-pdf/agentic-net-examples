using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string attachmentPath = "attachment_file.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);
        // Define the annotation rectangle (x, y, width, height)
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, 100, 100);
        // Create a file attachment annotation on page 1 with a Paperclip icon
        editor.CreateFileAttachment(rect, "Attached file", attachmentPath, 1, "Paperclip");
        editor.Save(outputPath);
        editor.Close();
        Console.WriteLine($"File attachment annotation added. Saved to '{outputPath}'.");
    }
}