using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document.
        Document pdfDoc = new Document(inputPath);

        // First external URL bookmark.
        OutlineItemCollection bookmark1 = new OutlineItemCollection(pdfDoc.Outlines)
        {
            Title = "Aspose PDF Documentation",
            Action = new GoToURIAction("https://docs.aspose.com/pdf/net/")
        };

        // Second external URL bookmark.
        OutlineItemCollection bookmark2 = new OutlineItemCollection(pdfDoc.Outlines)
        {
            Title = "Aspose PDF GitHub Repository",
            Action = new GoToURIAction("https://github.com/aspose-pdf/Aspose.PDF-for-.NET")
        };

        // Add bookmarks at the root level of the PDF.
        pdfDoc.Outlines.Add(bookmark1);
        pdfDoc.Outlines.Add(bookmark2);

        // Save the PDF with the new bookmarks.
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}