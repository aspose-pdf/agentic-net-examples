using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and the bookmark ID to be updated.
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string bookmarkId = "12 0 R";   // Example object ID of the bookmark.

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the PDF object by its ID.
            // The returned object can be cast to Aspose.Pdf.Bookmark if the ID refers to a bookmark.
            var pdfObject = doc.GetObjectById(bookmarkId);

            if (pdfObject is Bookmark bookmark)
            {
                // Update the destination page number to page 10 (1‑based indexing).
                bookmark.PageNumber = 10;

                // If the bookmark uses an explicit destination string, clear it so the
                // PageNumber takes effect. This is optional depending on the PDF.
                bookmark.Destination = string.Empty;
                Console.WriteLine($"Bookmark '{bookmark.Title}' now points to page {bookmark.PageNumber}.");
            }
            else
            {
                Console.Error.WriteLine($"No bookmark found with ID '{bookmarkId}'.");
            }

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPdf}'.");
    }
}