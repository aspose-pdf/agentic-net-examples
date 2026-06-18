using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // required for PdfContentEditor

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

        // Create the PdfContentEditor facade
        PdfContentEditor editor = new PdfContentEditor();

        // Load the PDF document into the editor
        editor.BindPdf(inputPath);

        // Parent bookmark for "Chapter One" (page 1)
        editor.CreateBookmarksAction(
            title: "Chapter One",
            color: System.Drawing.Color.Blue,
            boldFlag: true,
            italicFlag: false,
            file: null,
            actionType: "GoTo",
            destination: "1" // page number as string
        );

        // Child bookmark for "Section 1.1" (page 2)
        editor.CreateBookmarksAction(
            title: "Section 1.1",
            color: System.Drawing.Color.DarkGray,
            boldFlag: false,
            italicFlag: false,
            file: null,
            actionType: "GoTo",
            destination: "2"
        );

        // Child bookmark for "Section 1.2" (page 3)
        editor.CreateBookmarksAction(
            title: "Section 1.2",
            color: System.Drawing.Color.DarkGray,
            boldFlag: false,
            italicFlag: false,
            file: null,
            actionType: "GoTo",
            destination: "3"
        );

        // Save the PDF with the new bookmarks
        editor.Save(outputPath);

        // Release resources
        editor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}