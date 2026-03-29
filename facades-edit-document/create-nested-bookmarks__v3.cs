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

        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Parent bookmark for Chapter One (page 1)
        editor.CreateBookmarksAction(
            "Chapter One",
            System.Drawing.Color.DarkBlue,
            true,
            false,
            null,
            "GoTo",
            "1");

        // Subsection 1.1 (page 2)
        editor.CreateBookmarksAction(
            "Section 1.1",
            System.Drawing.Color.Green,
            false,
            false,
            null,
            "GoTo",
            "2");

        // Subsection 1.2 (page 3)
        editor.CreateBookmarksAction(
            "Section 1.2",
            System.Drawing.Color.Green,
            false,
            false,
            null,
            "GoTo",
            "3");

        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Nested bookmarks added and saved to '{outputPath}'.");
    }
}