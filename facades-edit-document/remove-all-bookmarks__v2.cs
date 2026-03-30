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

        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);
        editor.DeleteBookmarks();
        editor.Save(outputPath);
        Console.WriteLine($"All bookmarks removed. Saved to '{outputPath}'.");
    }
}