using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_bookmark.pdf";
        const string destName   = "MyNamedDestination";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF has no pages.");
                return;
            }

            // Define a named destination that points to page 1 (adjust as needed)
            Page targetPage = doc.Pages[1];
            GoToAction goTo = new GoToAction(targetPage);
            doc.NamedDestinations.Add(destName, goTo);

            // Create a bookmark that references the named destination
            PdfBookmarkEditor editor = new PdfBookmarkEditor();
            editor.BindPdf(doc);

            Bookmark bookmark = new Bookmark
            {
                Title      = "Jump to Named Destination",
                Action     = "Named",          // Use the "Named" action type
                Destination = destName         // Name of the destination defined above
            };

            // Add the bookmark to the document
            editor.CreateBookmarks(bookmark);

            // Save the updated PDF using the facade's Save method
            editor.Save(outputPath);
            editor.Close(); // optional, Dispose will be called automatically at the end of using if wrapped
        }

        Console.WriteLine($"Bookmark created and saved to '{outputPath}'.");
    }
}