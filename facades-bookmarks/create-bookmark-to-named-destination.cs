using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;   // NamedDestination, Bookmark

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF
        const string outputPath = "output.pdf";  // PDF with new bookmark
        const string destName   = "MyNamedDestination"; // name of the destination

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfBookmarkEditor facade (implements IDisposable)
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Access the underlying Document to ensure the destination page exists
            Document doc = editor.Document;
            if (doc.Pages.Count < 2) // example: we want the destination on page 2
            {
                Console.Error.WriteLine("The PDF does not contain page 2 for the named destination.");
                return;
            }

            // Create a named destination (adds it to the document's name dictionary)
            // The destination will point to page 2 (you can adjust as needed)
            // Note: NamedDestination only registers the name; the actual location is the page it is associated with.
            // Here we associate it with page 2 by creating a GoToAction later if needed.
            NamedDestination namedDest = new NamedDestination(doc, destName);
            // Optionally, you could create a GoToAction using the named destination:
            // GoToAction action = new GoToAction(doc, destName);
            // (Not required for the bookmark itself.)

            // Create a bookmark that references the named destination
            Bookmark bookmark = new Bookmark
            {
                Title      = $"Go to {destName}",
                Action     = "Named",   // indicates that Destination is a named destination
                Destination = destName   // the name defined above
                // No PageNumber is set because the action type is "Named"
            };

            // Add the bookmark to the PDF outline
            editor.CreateBookmarks(bookmark);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark pointing to named destination '{destName}' saved to '{outputPath}'.");
    }
}