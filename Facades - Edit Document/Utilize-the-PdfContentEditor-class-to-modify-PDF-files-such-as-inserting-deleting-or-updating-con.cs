using System;
using System.IO;
using System.Drawing; // Needed for Rectangle used by PdfContentEditor
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the editor and bind the source PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Delete all attachments from the document
            editor.DeleteAttachments();

            // Replace text "Old Text" with "New Text" on page 1
            editor.ReplaceText("Old Text", 1, "New Text");

            // Insert a web link annotation on page 2
            // PdfContentEditor.CreateWebLink expects System.Drawing.Rectangle
            System.Drawing.Rectangle linkRect = new System.Drawing.Rectangle(100, 500, 200, 50);
            editor.CreateWebLink(linkRect, "https://example.com", 2);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
