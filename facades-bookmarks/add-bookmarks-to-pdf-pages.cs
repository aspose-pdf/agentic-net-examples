using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            // Load the PDF document
            bookmarkEditor.BindPdf(inputPath);

            // Create a bookmark for page 1 with title "Intro"
            bookmarkEditor.CreateBookmarkOfPage("Intro", 1);

            // Create a bookmark for page 10 with title "Conclusion"
            bookmarkEditor.CreateBookmarkOfPage("Conclusion", 10);

            // Save the modified PDF
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
