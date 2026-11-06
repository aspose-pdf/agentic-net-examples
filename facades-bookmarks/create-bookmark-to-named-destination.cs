using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_bookmarked.pdf";
        const string bookmarkTitle = "See Section 2";
        const string namedDestination = "Section2Dest"; // Must exist in the source PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF with the Document API (the recommended way for outline/bookmark work)
        Document doc = new Document(inputPdf);

        // Create a top‑level outline item that points to the named destination.
        // The Action is a GoToAction that references the destination name.
        OutlineItemCollection bookmark = new OutlineItemCollection(doc.Outlines)
        {
            Title = bookmarkTitle,
            Action = new GoToAction(doc, namedDestination)
        };

        // Add the bookmark to the document's outline collection and save.
        doc.Outlines.Add(bookmark);
        doc.Save(outputPdf);

        Console.WriteLine($"Bookmark added and saved to '{outputPdf}'.");
    }
}