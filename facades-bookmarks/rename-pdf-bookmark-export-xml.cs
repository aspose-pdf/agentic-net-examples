using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "renamed.xml";
        const string oldTitle = "OldChapter";
        const string newTitle = "NewChapter";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfBookmarkEditor works with a limited set of methods. Use them as documented.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the PDF file to the editor.
            editor.BindPdf(inputPdf);

            // Rename the bookmark – the facade provides ModifyBookmarks(sourceTitle, destinationTitle).
            editor.ModifyBookmarks(oldTitle, newTitle);

            // Export the updated bookmark collection to an XML file. ExportBookmarksToXML is an instance method.
            editor.ExportBookmarksToXML(outputXml);

            // Close the editor (optional – the using statement will dispose it).
            editor.Close();
        }

        Console.WriteLine($"Bookmark renamed and exported to '{outputXml}'.");
    }
}
