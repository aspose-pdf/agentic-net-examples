using System;
using System.IO;
using System.Drawing; // Required for PdfBookmarkEditor.CreateBookmarks
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_with_bookmarks.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Initialize the bookmark editor and bind the source PDF
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                bookmarkEditor.BindPdf(inputPdfPath);

                // Create bookmarks for all pages using System.Drawing.Color (required by the API)
                // Parameters: Color, bold, italic
                bookmarkEditor.CreateBookmarks(Color.Blue, true, false);

                // Save the modified PDF with bookmarks
                bookmarkEditor.Save(outputPdfPath);
            }

            Console.WriteLine($"Bookmarks created successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}