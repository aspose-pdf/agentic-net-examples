using System;
using System.IO;
using System.Drawing;               // Required for System.Drawing.Color used by the facade
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string bookmarkTitle = "Example Org";
        const string url = "https://example.org";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use the PdfContentEditor facade to add a bookmark that opens an external URL.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF file.
            editor.BindPdf(inputPath);

            // Create a bookmark with a URI action.
            // Parameters: title, color, boldFlag, italicFlag, file (null for URI), actionType, destination.
            editor.CreateBookmarksAction(
                bookmarkTitle,
                System.Drawing.Color.Blue,   // Bookmark title color – must be System.Drawing.Color
                false,        // not bold
                false,        // not italic
                null,         // no external file needed for URI action
                "URI",        // action type for opening a URL
                url);         // the external URL to open

            // Save the modified PDF.
            editor.Save(outputPath);
            editor.Close();
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
