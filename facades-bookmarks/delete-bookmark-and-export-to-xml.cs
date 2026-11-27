using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXmlPath = "after_delete.xml";
        const string bookmarkToDelete = "Glossary";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Use a using block to ensure resources are released.
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                // Bind the PDF document.
                bookmarkEditor.BindPdf(inputPdfPath);

                // Delete the bookmark with the specified title.
                // The one‑argument overload deletes by title.
                bookmarkEditor.DeleteBookmarks(bookmarkToDelete);

                // Export the remaining bookmarks to an XML file.
                bookmarkEditor.ExportBookmarksToXML(outputXmlPath);

                // Optional: close the editor (Dispose will also close).
                bookmarkEditor.Close();
            }

            Console.WriteLine($"Bookmarks exported to '{outputXmlPath}' after deleting '{bookmarkToDelete}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
