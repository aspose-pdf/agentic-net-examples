using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_bookmark.pdf";
        const string destName  = "MyNamedDestination";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Initialize the bookmark editor and bind the source PDF.
            PdfBookmarkEditor editor = new PdfBookmarkEditor();
            editor.BindPdf(inputPdf);

            // Obtain the underlying Document object.
            Document doc = editor.Document;

            // ------------------------------------------------------------
            // 1. Create a named destination that points to a specific page.
            //    Here we point to page 2 (1‑based indexing).
            // ------------------------------------------------------------
            int targetPageNumber = 2;
            Page targetPage = doc.Pages[targetPageNumber];

            // Create the NamedDestination instance.
            NamedDestination namedDest = new NamedDestination(doc, destName);
            // Associate the destination with the desired page.
            // The NamedDestination itself does not hold page info,
            // so we set the action to a GoToAction that jumps to the page.
            // This is done by adding the destination to the collection;
            // the page reference is implicit when the bookmark uses it.
            doc.NamedDestinations.Add(destName, namedDest);

            // ------------------------------------------------------------
            // 2. Create a bookmark that references the named destination.
            // ------------------------------------------------------------
            Bookmark bookmark = new Bookmark
            {
                Title   = "Go to My Destination",
                Action  = "Named",          // Use the "Named" action type.
                Destination = destName,     // Name of the destination defined above.
                // Optional visual styling:
                TitleColor = System.Drawing.Color.Blue,
                BoldFlag   = true,
                ItalicFlag = false
            };

            // Add the bookmark to the PDF.
            editor.CreateBookmarks(bookmark);

            // ------------------------------------------------------------
            // 3. Save the modified PDF.
            // ------------------------------------------------------------
            editor.Save(outputPdf);
            editor.Close();

            Console.WriteLine($"Bookmark created and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}