using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string bookmarkTitle = "Bookmark To Delete";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the bookmark editor
        PdfBookmarkEditor editor = new PdfBookmarkEditor();

        // Load the PDF document
        editor.BindPdf(inputPath);

        // Delete the bookmark with the specified title
        editor.DeleteBookmarks(bookmarkTitle);

        // Save the updated PDF
        editor.Save(outputPath);

        // Release resources held by the editor
        editor.Close();

        Console.WriteLine($"Bookmark '{bookmarkTitle}' removed. Saved to '{outputPath}'.");
    }
}