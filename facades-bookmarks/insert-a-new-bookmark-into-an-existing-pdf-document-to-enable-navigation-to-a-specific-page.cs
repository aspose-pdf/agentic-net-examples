using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string bookmarkTitle = "Chapter 1";
        const int    pageNumber    = 1; // 1‑based page index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Initialize the bookmark editor on the loaded document
                PdfBookmarkEditor editor = new PdfBookmarkEditor(doc);

                // Create a new bookmark that points to the specified page
                editor.CreateBookmarkOfPage(bookmarkTitle, pageNumber);

                // Save the modified PDF to a new file
                editor.Save(outputPath);
            }

            Console.WriteLine($"Bookmark '{bookmarkTitle}' added to page {pageNumber} and saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}