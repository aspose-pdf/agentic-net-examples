using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_attachments.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the PdfContentEditor facade (provided by Aspose.Pdf.Facades)
        PdfContentEditor editor = new PdfContentEditor();

        // Load the PDF document into the editor
        editor.BindPdf(inputPath);

        // Remove all embedded attachments from the PDF
        editor.DeleteAttachments();

        // Save the resulting PDF without attachments
        editor.Save(outputPath);

        Console.WriteLine($"Attachments removed successfully. Output saved to '{outputPath}'.");
    }
}