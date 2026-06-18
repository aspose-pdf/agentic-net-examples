using System;
using System.IO;
using System.Drawing;               // Required for Color parameters
using Aspose.Pdf.Facades;          // Facade API for bookmark actions

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_bookmarks.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfContentEditor facade to add bookmarks with external URL actions
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Initialize the facade with the source PDF
            editor.BindPdf(inputPdf);

            // Bookmark linking to Aspose website (bold, blue)
            editor.CreateBookmarksAction(
                title:       "Aspose Home",
                color:       Color.Blue,
                boldFlag:    true,
                italicFlag:  false,
                file:        null,               // Not needed for URI action
                actionType:  "URI",
                destination: "https://www.aspose.com");

            // Bookmark linking to GitHub (italic, green)
            editor.CreateBookmarksAction(
                title:       "GitHub",
                color:       Color.Green,
                boldFlag:    false,
                italicFlag:  true,
                file:        null,
                actionType:  "URI",
                destination: "https://github.com");

            // Persist changes to a new PDF file
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmarks with external URLs saved to '{outputPdf}'.");
    }
}