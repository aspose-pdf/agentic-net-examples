using System;
using System.Drawing; // for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for GoToURIAction

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string bookmarkTitle = "Help";
        const string url = "https://docs.aspose.com/pdf/net/";

        // ---------------------------------------------------------------------
        // 1. Create a minimal PDF so the sandbox has a file to open.
        // ---------------------------------------------------------------------
        using (Document seed = new Document())
        {
            seed.Pages.Add(); // at least one page is required
            seed.Save(inputPath);
        }

        // ---------------------------------------------------------------------
        // 2. Load the PDF and add a bookmark that opens the online documentation.
        // ---------------------------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            // Create a new outline (bookmark) entry.
            OutlineItemCollection bookmark = new OutlineItemCollection(doc.Outlines)
            {
                Title = bookmarkTitle,
                // Title colour expects System.Drawing.Color when both namespaces are imported.
                Color = System.Drawing.Color.Blue,
                // Define the action that will be performed when the bookmark is clicked.
                Action = new GoToURIAction(url)
            };

            // Add the bookmark to the document's outline collection.
            doc.Outlines.Add(bookmark);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bookmark '{bookmarkTitle}' added. Output saved to '{outputPath}'.");
    }
}
