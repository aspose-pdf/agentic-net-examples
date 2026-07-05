using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // Required by CreateBookmarksAction (expects System.Drawing.Color)

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

        // Use PdfContentEditor facade to add a bookmark that opens an external URL
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPath);

            // Create a bookmark with a URI action.
            // Parameters: title, color, boldFlag, italicFlag, file (null for URI), actionType, destination URL
            editor.CreateBookmarksAction(
                "Example Org",          // Bookmark title
                Color.Blue,            // Title color (System.Drawing.Color required by the API)
                false,                 // Bold flag
                false,                 // Italic flag
                null,                  // File parameter not needed for URI action
                "URI",                 // Action type for external URL
                "https://example.org"  // Destination URL
            );

            // Save the modified PDF with the new bookmark
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}