using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_attachments.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // PdfContentEditor does not implement IDisposable, so no using block is needed.
            PdfContentEditor editor = new PdfContentEditor();

            // Load the PDF file.
            editor.BindPdf(inputPath);

            // Remove all embedded file attachments.
            editor.DeleteAttachments();

            // Save the PDF without attachments.
            editor.Save(outputPath);

            Console.WriteLine($"Attachments removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}