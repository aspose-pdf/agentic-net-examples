using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string helpUrl = "https://example.com/documentation";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            PdfBookmarkEditor editor = new PdfBookmarkEditor(doc);

            Aspose.Pdf.Facades.Bookmark helpBookmark = new Aspose.Pdf.Facades.Bookmark();
            helpBookmark.Title = "Help";
            helpBookmark.Action = "URI"; // open a URL when clicked
            helpBookmark.Destination = helpUrl;

            editor.CreateBookmarks(helpBookmark);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}