using System;
using System.Drawing;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF and create bookmarks using PdfContentEditor
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Parent bookmark: Chapter One (page 1)
            editor.CreateBookmarksAction(
                title: "Chapter One",
                color: System.Drawing.Color.DarkBlue,
                boldFlag: true,
                italicFlag: false,
                file: null,
                actionType: "GoTo",
                destination: "1");

            // Subsection 1.1 (page 2)
            editor.CreateBookmarksAction(
                title: "1.1 Introduction",
                color: System.Drawing.Color.Black,
                boldFlag: false,
                italicFlag: false,
                file: null,
                actionType: "GoTo",
                destination: "2");

            // Subsection 1.2 (page 3)
            editor.CreateBookmarksAction(
                title: "1.2 Details",
                color: System.Drawing.Color.Black,
                boldFlag: false,
                italicFlag: false,
                file: null,
                actionType: "GoTo",
                destination: "3");

            // Save the PDF with the new bookmarks
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}