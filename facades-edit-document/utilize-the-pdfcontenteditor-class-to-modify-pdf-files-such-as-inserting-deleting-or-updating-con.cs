using System;
using System.IO;
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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfContentEditor provides methods to edit existing PDFs.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF file.
            editor.BindPdf(inputPath);

            // Example 1: Delete all file attachments from the document.
            editor.DeleteAttachments();

            // Example 2: Replace a specific text on page 1.
            // This replaces the first occurrence of "Hello" with "Hi" on page 1.
            editor.ReplaceText("Hello", 1, "Hi");

            // Example 3: Insert a web link annotation on page 1.
            // Define the clickable rectangle area (x, y, width, height).
            // Note: PdfContentEditor.CreateWebLink expects System.Drawing.Rectangle.
            System.Drawing.Rectangle linkRect = new System.Drawing.Rectangle(100, 500, 200, 20);
            // Create a link that points to an external URL.
            editor.CreateWebLink(linkRect, "https://www.example.com", 1);

            // Save the modified PDF to the output path.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
