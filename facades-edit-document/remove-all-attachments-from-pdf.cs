using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_attachments.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor implements IDisposable, so wrap it in a using block
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPath);

            // Remove all embedded attachments from the document
            editor.DeleteAttachments();

            // Persist the changes to a new file
            editor.Save(outputPath);
        }

        Console.WriteLine($"All attachments removed. Output saved to '{outputPath}'.");
    }
}