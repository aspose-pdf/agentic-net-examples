using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        // PdfBookmarkEditor does not implement IDisposable, so we instantiate it directly
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Bind the PDF document to the editor
        bookmarkEditor.BindPdf(inputPath);

        // Rename the bookmark titled "Section2" to "Overview"
        // The facade provides a Rename‑like method called ModifyBookmarks(sourceTitle, destinationTitle)
        bookmarkEditor.ModifyBookmarks("Section2", "Overview");

        // Save the modified PDF to a new file
        bookmarkEditor.Save(outputPath);
        // Optionally close the editor (not strictly required, but good practice)
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmark renamed and saved to '{outputPath}'.");
    }
}
