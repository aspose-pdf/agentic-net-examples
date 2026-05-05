using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_no_bookmarks.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Create the bookmark editor facade
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

            // Load the PDF document into the editor
            bookmarkEditor.BindPdf(inputPdf);

            // Delete all bookmarks
            bookmarkEditor.DeleteBookmarks();

            // Save the modified PDF
            bookmarkEditor.Save(outputPdf);

            // Release resources held by the facade
            bookmarkEditor.Close();

            Console.WriteLine($"Bookmarks removed. Saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}