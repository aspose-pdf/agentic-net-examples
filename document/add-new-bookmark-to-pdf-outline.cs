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
        const string headingTitle = "New Section";
        const int targetPageNumber = 2; // page where the bookmark should point (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Validate page number (Aspose.Pdf uses 1‑based indexing)
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Target page number is out of range.");
                return;
            }

            // Get the target page instance
            Page targetPage = doc.Pages[targetPageNumber];

            // Create a GoToAction that points to the desired page.
            // The constructor expects a Page object; the destination type defaults to FitH.
            GoToAction goTo = new GoToAction(targetPage);

            // Create a new outline (bookmark) item. The constructor expects the parent OutlineCollection.
            OutlineItemCollection newOutline = new OutlineItemCollection(doc.Outlines)
            {
                Title = headingTitle,
                Action = goTo,
                Italic = true // optional visual style
            };

            // Add the new outline to the document's root outline collection.
            doc.Outlines.Add(newOutline);

            // Save the updated PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Outline updated and saved to '{outputPath}'.");
    }
}
