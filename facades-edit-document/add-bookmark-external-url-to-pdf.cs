using System;
using System.IO;
using System.Drawing;               // for Color
using Aspose.Pdf.Facades;          // PdfContentEditor facade

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

        // Use PdfContentEditor to bind the PDF, add a bookmark with a URI action, and save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Create a bookmark that opens an external URL when selected.
            editor.CreateBookmarksAction(
                title:       "Example Org",          // bookmark title
                color:       Color.Blue,            // title color
                boldFlag:    false,                 // not bold
                italicFlag:  false,                 // not italic
                file:        null,                  // not needed for URI action
                actionType:  "URI",                 // action type for external link
                destination: "https://example.org"  // target URL
            );

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}