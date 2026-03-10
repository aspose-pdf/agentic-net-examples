using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF with existing bookmark
        const string outputPdf = "output_modified.pdf"; // PDF after bookmark title change
        const string oldTitle  = "Existing Bookmark Title";
        const string newTitle  = "Updated Bookmark Title";

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdf}");
            return;
        }

        try
        {
            // Initialize the bookmark editor facade
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

            // Load the PDF document into the editor
            bookmarkEditor.BindPdf(inputPdf);

            // Modify the bookmark title from oldTitle to newTitle
            bookmarkEditor.ModifyBookmarks(oldTitle, newTitle);

            // Save the modified PDF to a new file
            bookmarkEditor.Save(outputPdf);

            // Optional: release resources held by the editor
            bookmarkEditor.Close();

            Console.WriteLine($"Bookmark title updated and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}