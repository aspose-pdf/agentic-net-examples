using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the editor, bind the source PDF, delete all attachments, and save.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);
        editor.DeleteAttachments(); // removes all embedded file attachments
        editor.Save(outputPath);    // persists the changes

        Console.WriteLine($"Attachments removed. Saved to '{outputPath}'.");
    }
}