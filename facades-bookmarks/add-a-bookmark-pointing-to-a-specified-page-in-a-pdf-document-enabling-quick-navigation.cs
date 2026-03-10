using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // PDF with the new bookmark
        const string bookmarkTitle = "Chapter 1"; // bookmark name
        const int targetPage = 1;               // page to navigate to (1‑based)

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create the bookmark editor (factory method)
        PdfBookmarkEditor editor = new PdfBookmarkEditor();

        // Load the PDF into the editor (provided load rule)
        editor.BindPdf(inputPdf);

        // Add a bookmark that points to the specified page (provided create rule)
        editor.CreateBookmarkOfPage(bookmarkTitle, targetPage);

        // Save the modified PDF (provided save rule)
        editor.Save(outputPdf);

        // Release any resources held by the editor
        editor.Close();

        Console.WriteLine($"Bookmark \"{bookmarkTitle}\" added to page {targetPage}. Saved as \"{outputPdf}\".");
    }
}