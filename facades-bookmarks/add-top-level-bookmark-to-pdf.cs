using System;
using System.Drawing; // for System.Drawing.Color
using Aspose.Pdf.Facades; // PdfBookmarkEditor, Bookmark

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_bookmark.pdf";

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfBookmarkEditor to add a top‑level bookmark
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdf);

            // Create a bookmark that points to page 1
            Bookmark bm = new Bookmark
            {
                Title      = "First Page",
                PageNumber = 1,
                TitleColor = System.Drawing.Color.Blue // Bookmark title color
            };

            // Add the bookmark to the document
            editor.CreateBookmarks(bm);

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPdf}'.");
    }
}