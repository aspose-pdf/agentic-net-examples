using System;
using System.IO;
using System.Drawing;               // Required for Color parameter
using Aspose.Pdf.Facades;          // PdfContentEditor namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor is a disposable facade; use a using block for deterministic cleanup
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPath);

            // ----- Parent bookmark: Chapter One (page 1) -----
            editor.CreateBookmarksAction(
                title:       "Chapter One",
                color:       Color.Blue,
                boldFlag:    true,
                italicFlag:  false,
                file:        null,          // Not needed for GoTo action
                actionType:  "GoTo",
                destination: "1");          // Destination page as string

            // ----- Subsection 1.1 (page 2) -----
            editor.CreateBookmarksAction(
                title:       "1.1 Introduction",
                color:       Color.DarkGray,
                boldFlag:    false,
                italicFlag:  false,
                file:        null,
                actionType:  "GoTo",
                destination: "2");

            // ----- Subsection 1.2 (page 3) -----
            editor.CreateBookmarksAction(
                title:       "1.2 Details",
                color:       Color.DarkGray,
                boldFlag:    false,
                italicFlag:  false,
                file:        null,
                actionType:  "GoTo",
                destination: "3");

            // Save the PDF with the newly added bookmarks
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}