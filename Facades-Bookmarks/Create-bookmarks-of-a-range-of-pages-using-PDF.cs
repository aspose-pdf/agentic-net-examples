using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output PDF file path (will contain the new bookmarks)
        const string outputPdf = "output_bookmarked.pdf";

        // Define the page range for which bookmarks will be created (inclusive)
        int startPage = 5;
        int endPage   = 10;

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Prepare arrays of bookmark titles and corresponding page numbers
        int count = endPage - startPage + 1;
        string[] titles = new string[count];
        int[]    pages  = new int[count];

        for (int i = 0; i < count; i++)
        {
            int pageNumber = startPage + i;
            titles[i] = $"Page {pageNumber}";
            pages[i]  = pageNumber;
        }

        // Use PdfBookmarkEditor (Facades API) to bind the PDF, create bookmarks, and save
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);                     // Load the PDF
        editor.CreateBookmarkOfPage(titles, pages);   // Create bookmarks for the specified range
        editor.Save(outputPdf);                       // Save the modified PDF
        editor.Close();                               // Release resources bound to the editor

        Console.WriteLine($"Bookmarks added for pages {startPage}-{endPage} and saved to '{outputPdf}'.");
    }
}