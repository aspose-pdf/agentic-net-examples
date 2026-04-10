using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the existing PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a new top‑level outline item (bookmark) and associate it with the document's outline collection
            OutlineItemCollection newOutline = new OutlineItemCollection(doc.Outlines)
            {
                Title = "New Section"
            };

            // Define the action that will be performed when the bookmark is clicked.
            // Here we navigate to page 2 (1‑based indexing) and fit the page to the window.
            newOutline.Action = new GoToAction(doc.Pages[2]);

            // Add the new outline item to the document's outline hierarchy.
            doc.Outlines.Add(newOutline);

            // Save the updated PDF. The outline (bookmark) is now part of the document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with new outline saved to '{outputPath}'.");
    }
}