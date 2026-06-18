using System;
using System.IO;
using System.Drawing;               // Rectangle for link area
using Aspose.Pdf.Facades;          // PdfContentEditor

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

        // Create a PdfContentEditor, bind the source PDF, add a web link on page 2, and save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document into the editor.
            editor.BindPdf(inputPath);

            // Define the clickable rectangle (x, y, width, height).
            // Adjust coordinates as needed for your document.
            Rectangle linkRect = new Rectangle(100, 500, 200, 50);

            // Add a hyperlink that opens the specified URL when clicked on page 2.
            editor.CreateWebLink(linkRect, "https://example.com", 2);

            // Persist the changes to a new file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Hyperlink added and saved to '{outputPath}'.");
    }
}