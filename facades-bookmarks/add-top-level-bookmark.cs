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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (Document document = new Document(inputPath))
        {
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor(document);

            Bookmark bookmark = new Bookmark
            {
                Title = "First Page",
                PageNumber = 1,
                // TitleColor expects System.Drawing.Color, not Aspose.Pdf.Color
                TitleColor = System.Drawing.Color.Blue
            };

            bookmarkEditor.CreateBookmarks(bookmark);
            bookmarkEditor.Save(outputPath);
            bookmarkEditor.Close();
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}