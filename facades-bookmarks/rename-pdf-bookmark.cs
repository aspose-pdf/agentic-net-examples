using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        try
        {
            // PdfBookmarkEditor implements IDisposable, so wrap it in a using block.
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                // Bind the existing PDF document.
                editor.BindPdf(inputPath);

                // Rename the bookmark titled "Chapter1" to "Introduction".
                // PdfBookmarkEditor provides a ModifyBookmarks(sourceTitle, destinationTitle) method for renaming.
                editor.ModifyBookmarks("Chapter1", "Introduction");

                // Save the updated PDF.
                editor.Save(outputPath);
            }

            Console.WriteLine($"Bookmark updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
