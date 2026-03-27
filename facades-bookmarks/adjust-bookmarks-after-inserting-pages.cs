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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Insert two blank pages at the beginning (position 1 is the first page)
            doc.Pages.Insert(1);
            doc.Pages.Insert(1);

            // Adjust bookmarks: remove existing ones and recreate with shifted page numbers
            PdfBookmarkEditor editor = new PdfBookmarkEditor(doc);
            editor.DeleteBookmarks();

            // Example bookmark titles (in a real scenario these could be read from the original document)
            string[] titles = new string[] { "First Chapter", "Second Chapter", "Third Chapter" };
            // Original pages were 1, 2, 3; after inserting two pages they become 3, 4, 5
            int[] newPageNumbers = new int[] { 3, 4, 5 };
            editor.CreateBookmarkOfPage(titles, newPageNumbers);

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
