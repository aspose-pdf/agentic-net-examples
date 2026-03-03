using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string bookmarkTitle = "Chapter 1";
        const int pageNumber = 1; // Aspose.Pdf uses 1‑based page indexing

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the bookmark editor and bind the source PDF
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);

        // Create a bookmark that points to the specified page
        editor.CreateBookmarkOfPage(bookmarkTitle, pageNumber);

        // Save the PDF with the new bookmark
        editor.Save(outputPdf);

        // Release resources held by the editor
        editor.Close();

        Console.WriteLine($"Bookmark '{bookmarkTitle}' added to page {pageNumber} and saved as '{outputPdf}'.");
    }
}