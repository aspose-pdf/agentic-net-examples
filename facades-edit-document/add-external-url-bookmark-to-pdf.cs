using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // required for System.Drawing.Color used by PdfContentEditor

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string bookmarkTitle = "Example Site";
        const string url = "https://example.org";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the facade and bind the source PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Create a bookmark that opens an external URL (action type "URI")
        // Parameters: title, color, boldFlag, italicFlag, file (null for URI), actionType, destination (URL)
        editor.CreateBookmarksAction(
            bookmarkTitle,
            Color.Blue,
            false,
            false,
            null,
            "URI",
            url);

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}