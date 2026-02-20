using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for input PDF, output PDF and temporary XML export
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";
        string exportXmlPath = "bookmarks.xml";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        try
        {
            // Work with bookmarks using PdfBookmarkEditor
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                // Load the PDF document
                bookmarkEditor.BindPdf(inputPath);

                // Remove any existing bookmarks
                bookmarkEditor.DeleteBookmarks();

                // Create a simple bookmark for page 1
                bookmarkEditor.CreateBookmarkOfPage("First Page", 1);

                // Build a nested bookmark hierarchy
                Bookmark parent = new Bookmark
                {
                    Title = "Parent Bookmark"
                    // Styling properties (Color, Bold, Italic) are not part of the current Bookmark API.
                    // If styling is required, use the TextStyle property of the Bookmark (not shown here).
                };

                Bookmark child = new Bookmark
                {
                    Title = "Child Bookmark"
                };

                // Attach child to parent
                parent.ChildItems.Add(child);

                // Add the hierarchy to the document
                bookmarkEditor.CreateBookmarks(parent);

                // Export current bookmarks to an XML file
                bookmarkEditor.ExportBookmarksToXML(exportXmlPath);

                // Modify a bookmark title (example: rename "First Page" to "Cover Page")
                bookmarkEditor.ModifyBookmarks("First Page", "Cover Page");

                // Import bookmarks back from the XML file
                bookmarkEditor.ImportBookmarksWithXML(exportXmlPath);

                // Save the modified PDF
                bookmarkEditor.Save(outputPath);
            }

            Console.WriteLine($"Bookmarks processed and PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}