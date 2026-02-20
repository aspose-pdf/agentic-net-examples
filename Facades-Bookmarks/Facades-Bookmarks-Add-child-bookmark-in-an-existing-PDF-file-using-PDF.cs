using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class AddChildBookmark
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdf}");
            return;
        }

        try
        {
            // Initialize the bookmark editor facade
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                // Bind the existing PDF document
                bookmarkEditor.BindPdf(inputPdf);

                // ------------------------------------------------------------
                // Create the parent bookmark
                // ------------------------------------------------------------
                Bookmark parentBookmark = new Bookmark
                {
                    Title = "Parent Outline",
                    // Optional visual style
                    // Color = Color.Blue,
                    // Bold = true,
                    // Italic = false
                };

                // ------------------------------------------------------------
                // Create the child bookmark that points to page 2
                // ------------------------------------------------------------
                // Ensure the document has at least two pages
                if (bookmarkEditor.Document.Pages.Count < 2)
                {
                    Console.Error.WriteLine("Error: The PDF does not contain a page 2 for the child bookmark.");
                    return;
                }

                // Build an explicit destination for page 2
                FitExplicitDestination page2Dest = new FitExplicitDestination(
                    bookmarkEditor.Document.Pages[2]); // 1‑based index

                Bookmark childBookmark = new Bookmark
                {
                    Title = "Child Outline",
                    // Destination must be a string representation of the explicit destination
                    Destination = page2Dest.ToString()
                };

                // Attach the child to the parent
                parentBookmark.ChildItems.Add(childBookmark);

                // ------------------------------------------------------------
                // Add the hierarchical bookmark structure to the PDF
                // ------------------------------------------------------------
                // The CreateBookmarks(Bookmark) overload creates nested bookmarks.
                bookmarkEditor.CreateBookmarks(parentBookmark);

                // Save the modified PDF using the provided document‑save rule
                bookmarkEditor.Save(outputPdf);
            }

            Console.WriteLine($"Child bookmark added successfully. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}