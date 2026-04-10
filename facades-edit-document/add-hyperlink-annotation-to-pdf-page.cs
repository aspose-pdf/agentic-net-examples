using System;
using System.IO;
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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the editor, add a web link on page 2, and save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // System.Drawing.Rectangle defining the clickable area (x, y, width, height).
            System.Drawing.Rectangle linkRect = new System.Drawing.Rectangle(100, 500, 200, 50);

            // Create a hyperlink that opens the specified URL when clicked.
            editor.CreateWebLink(linkRect, "https://example.com", 2);

            // Persist the changes.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Hyperlink added and saved to '{outputPath}'.");
    }
}
