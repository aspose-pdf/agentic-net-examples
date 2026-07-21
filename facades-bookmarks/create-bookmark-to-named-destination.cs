using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string destinationName = "MyDestination";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // 1. Define a named destination.
            //    Here we create a FitExplicitDestination on page 2
            //    (adjust the page number as needed).
            // ------------------------------------------------------------
            Page targetPage = doc.Pages[2]; // 1‑based indexing
            FitExplicitDestination fitDest = new FitExplicitDestination(targetPage);

            // Add the named destination to the document's collection
            doc.NamedDestinations.Add(destinationName, fitDest);

            // ------------------------------------------------------------
            // 2. Create a bookmark that points to the named destination.
            // ------------------------------------------------------------
            Bookmark bookmark = new Bookmark
            {
                Title = "Jump to Named Destination",
                Action = "Named",          // Action type for named destinations
                Destination = destinationName,
                BoldFlag = true,
                ItalicFlag = false
            };

            // Use PdfBookmarkEditor to insert the bookmark into the PDF
            PdfBookmarkEditor editor = new PdfBookmarkEditor(doc);
            editor.CreateBookmarks(bookmark);
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF with bookmark saved to '{outputPath}'.");
    }
}