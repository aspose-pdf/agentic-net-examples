using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_attachments.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor is a facade that implements IDisposable, so use a using block
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the PDF file to the editor
            editor.BindPdf(inputPath);

            // Delete all embedded attachments
            editor.DeleteAttachments();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"All attachments removed. Output saved to '{outputPath}'.");
    }
}