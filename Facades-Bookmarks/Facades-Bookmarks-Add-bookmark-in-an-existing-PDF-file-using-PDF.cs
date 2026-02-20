using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path (will contain the new bookmark)
        const string outputPath = "output.pdf";
        // Title of the bookmark to be added
        const string bookmarkTitle = "My Bookmark";
        // Page number the bookmark should point to (1‑based indexing)
        const int pageNumber = 1;

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Create the PdfBookmarkEditor facade
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

            // Load (bind) the existing PDF document
            bookmarkEditor.BindPdf(inputPath);

            // Add a bookmark that points to the specified page
            bookmarkEditor.CreateBookmarkOfPage(bookmarkTitle, pageNumber);

            // Save the modified PDF to the output file
            bookmarkEditor.Save(outputPath);

            // Release resources associated with the facade
            bookmarkEditor.Close();

            Console.WriteLine($"Bookmark \"{bookmarkTitle}\" added to page {pageNumber} and saved as \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}