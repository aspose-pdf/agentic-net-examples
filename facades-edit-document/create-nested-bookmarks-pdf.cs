using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // Fully qualified System.Drawing.Color to avoid ambiguity

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

        // Use PdfContentEditor to add bookmarks
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF
            editor.BindPdf(inputPath);

            // Parent bookmark: Chapter One (page 1)
            editor.CreateBookmarksAction(
                title: "Chapter One",
                color: System.Drawing.Color.Blue,
                boldFlag: true,
                italicFlag: false,
                file: null,
                actionType: "GoTo",
                destination: "1");

            // Subsection 1.1 (page 2)
            editor.CreateBookmarksAction(
                title: "Section 1.1",
                color: System.Drawing.Color.DarkGreen,
                boldFlag: false,
                italicFlag: false,
                file: null,
                actionType: "GoTo",
                destination: "2");

            // Subsection 1.2 (page 3)
            editor.CreateBookmarksAction(
                title: "Section 1.2",
                color: System.Drawing.Color.DarkGreen,
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