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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the bookmark editor and bind the PDF file
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Create a parent bookmark that will contain chapter bookmarks
        Bookmark parent = new Bookmark();
        parent.Title = "Chapters";
        parent.Action = "GoTo";
        parent.PageNumber = 1; // optional – points to the first page

        // Create child bookmarks for each chapter (example pages 2, 5 and 8)
        Bookmark chapter1 = new Bookmark();
        chapter1.Title = "Chapter 1";
        chapter1.PageNumber = 2;
        chapter1.Action = "GoTo";

        Bookmark chapter2 = new Bookmark();
        chapter2.Title = "Chapter 2";
        chapter2.PageNumber = 5;
        chapter2.Action = "GoTo";

        Bookmark chapter3 = new Bookmark();
        chapter3.Title = "Chapter 3";
        chapter3.PageNumber = 8;
        chapter3.Action = "GoTo";

        // Assemble the child collection and attach it to the parent
        Bookmarks childCollection = new Bookmarks();
        childCollection.Add(chapter1);
        childCollection.Add(chapter2);
        childCollection.Add(chapter3);
        parent.ChildItem = childCollection;

        // Add the hierarchical bookmark structure to the PDF
        editor.CreateBookmarks(parent);

        // Save the modified PDF and release resources
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
