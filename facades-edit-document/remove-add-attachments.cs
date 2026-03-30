using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the content editor and bind the existing PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Remove all existing attachments
        editor.DeleteAttachments();

        // Add fresh attachments to the document
        editor.AddDocumentAttachment("newAttachment1.pdf", "First new attachment");
        editor.AddDocumentAttachment("newAttachment2.pdf", "Second new attachment");

        // Save the updated PDF
        editor.Save(outputPath);

        Console.WriteLine($"Attachments refreshed and saved to '{outputPath}'.");
    }
}