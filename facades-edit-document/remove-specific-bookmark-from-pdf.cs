using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string bookmarkTitle = "Draft Outline";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the bookmark editor and bind the PDF file
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Delete the bookmark with the specified title
        editor.DeleteBookmarks(bookmarkTitle);

        // Save the modified PDF
        editor.Save(outputPath);

        // Release resources held by the editor
        editor.Close();

        Console.WriteLine($"Bookmark \"{bookmarkTitle}\" removed. Saved to '{outputPath}'.");
    }
}