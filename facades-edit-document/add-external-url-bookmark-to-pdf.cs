using System;
using System.IO;
using System.Drawing;               // For System.Drawing.Color
using Aspose.Pdf.Facades;          // PdfContentEditor resides here

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

        // Bind the existing PDF, add a bookmark that opens an external URL, and save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Create a bookmark titled "Example Org" that launches the URL https://example.org.
            // Parameters: title, title color, bold flag, italic flag, file (null for URI), action type, destination.
            editor.CreateBookmarksAction(
                "Example Org",          // Bookmark title
                Color.Blue,            // Title color
                false,                 // Bold flag
                false,                 // Italic flag
                null,                  // No file needed for URI action
                "URI",                 // Action type for external link
                "https://example.org"  // Destination URL
            );

            // Persist the changes to a new PDF file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}