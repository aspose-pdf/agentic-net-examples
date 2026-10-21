using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string helpUrl = "https://example.com/documentation";

        // Ensure the input PDF exists – create a minimal one if it does not.
        if (!File.Exists(inputPath))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add(); // at least one page
            placeholder.Save(inputPath);
        }

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // Create a bookmark (outline item) that opens an external URL
        var helpBookmark = new OutlineItemCollection(pdfDocument.Outlines)
        {
            Title = "Help",
            Action = new GoToURIAction(helpUrl)
        };

        // Add the bookmark to the document's outline tree
        pdfDocument.Outlines.Add(helpBookmark);

        // Save the modified PDF
        pdfDocument.Save(outputPath);
    }
}
