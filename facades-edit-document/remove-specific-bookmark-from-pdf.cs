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
        const string bookmarkTitle = "Draft Outline";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor handles bookmark manipulation
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPath);

            // Remove the bookmark with the specified title
            editor.DeleteBookmarks(bookmarkTitle);

            // Persist the changes to a new file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark \"{bookmarkTitle}\" removed. Saved to \"{outputPath}\".");
    }
}