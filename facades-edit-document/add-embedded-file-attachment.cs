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

        // Load the PDF, embed the file, and save the result
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);
            // Adds the file as an embedded attachment (no visible annotation)
            editor.AddDocumentAttachment(attachmentPath, "Sample attachment description");
            editor.Save(outputPath);
        }

        Console.WriteLine($"Embedded attachment added and saved to '{outputPath}'.");
    }
}
