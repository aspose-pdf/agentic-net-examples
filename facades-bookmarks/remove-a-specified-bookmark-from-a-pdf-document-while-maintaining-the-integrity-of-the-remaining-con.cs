using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "output.pdf";         // PDF after bookmark removal
        const string bookmarkTitle = "Bookmark To Remove"; // title of the bookmark to delete

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Initialize the bookmark editor facade
        PdfBookmarkEditor editor = new PdfBookmarkEditor();

        // Bind the PDF document to the editor
        editor.BindPdf(inputPath);

        // Delete the specified bookmark (if it exists)
        editor.DeleteBookmarks(bookmarkTitle);

        // Save the modified PDF to a new file
        editor.Save(outputPath);

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"Bookmark \"{bookmarkTitle}\" removed. Output saved to '{outputPath}'.");
    }
}