using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string oldTitle   = "Existing Bookmark Title";
        const string newTitle   = "Updated Bookmark Title";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor implements IDisposable, so use a using block for deterministic cleanup
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPath);

            // Change the title of the specified bookmark
            editor.ModifyBookmarks(oldTitle, newTitle);

            // Persist the changes to a new file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark '{oldTitle}' updated to '{newTitle}' and saved as '{outputPath}'.");
    }
}