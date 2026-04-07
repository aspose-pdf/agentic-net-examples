using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing;

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

        // Bind the PDF and create a hierarchy of bookmarks.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Parent bookmark for Chapter One (page 1).
            editor.CreateBookmarksAction(
                "Chapter One",
                Color.Blue,
                true,
                false,
                null,
                "GoTo",
                "1");

            // Child bookmark for Section 1.1 (page 2).
            editor.CreateBookmarksAction(
                "Section 1.1",
                Color.DarkGray,
                false,
                false,
                null,
                "GoTo",
                "2");

            // Child bookmark for Section 1.2 (page 3).
            editor.CreateBookmarksAction(
                "Section 1.2",
                Color.DarkGray,
                false,
                false,
                null,
                "GoTo",
                "3");

            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}