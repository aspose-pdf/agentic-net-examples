using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string bookmarkTitle = "Project Overview";
        const int targetPage = 5;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            PdfBookmarkEditor editor = new PdfBookmarkEditor();
            editor.BindPdf(inputPath);
            editor.CreateBookmarkOfPage(bookmarkTitle, targetPage);
            editor.Save(outputPath);
            editor.Close();
            Console.WriteLine($"Bookmark '{bookmarkTitle}' added to page {targetPage}. Saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}