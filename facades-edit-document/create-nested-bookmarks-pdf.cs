using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // required for GoToAction

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "bookmarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Bind the document to PdfContentEditor (required for outline operations)
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // ----- Create the parent bookmark (Chapter One) -----
            // OutlineItemCollection represents a single outline entry.
            OutlineItemCollection chapterOne = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Chapter One",
                Open   = false // collapsed by default
            };

            // ----- Create a subsection bookmark (Section 1.1) -----
            OutlineItemCollection section11 = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Section 1.1",
                // Go to page 2 when this bookmark is activated
                Action = new GoToAction(2)
            };

            // ----- Create another subsection bookmark (Section 1.2) -----
            OutlineItemCollection section12 = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Section 1.2",
                // Go to page 3 when this bookmark is activated
                Action = new GoToAction(3)
            };

            // Attach child bookmarks to the parent
            chapterOne.Add(section11);
            chapterOne.Add(section12);

            // Add the top‑level bookmark to the document's outline collection
            doc.Outlines.Add(chapterOne);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
