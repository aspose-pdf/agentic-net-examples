using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string attachmentPath = "attachment.txt";
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

        // Unicode description containing characters from various scripts
        string description = "Описание вложения – тест 🚀 中文字符";

        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);
            editor.AddDocumentAttachment(attachmentPath, description);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Attachment added with Unicode description. Saved to '{outputPath}'.");
    }
}