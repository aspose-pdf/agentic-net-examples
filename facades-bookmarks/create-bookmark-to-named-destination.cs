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
        const string bookmarkTitle = "Reference to Section 2";
        const string destinationName = "Section2Dest"; // Named destination already defined in the PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Create a GoToAction that points to the existing named destination
        GoToAction goTo = new GoToAction(doc, destinationName);

        // Create the bookmark (outline item) and assign the action
        OutlineItemCollection bookmark = new OutlineItemCollection(doc.Outlines)
        {
            Title = bookmarkTitle,
            Action = goTo
        };

        // Add the bookmark to the document outline
        doc.Outlines.Add(bookmark);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}