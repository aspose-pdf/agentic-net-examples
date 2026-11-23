using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for explicit destination types

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "figbookmark.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the top‑level outline collection (root)
            OutlineCollection rootOutlines = doc.Outlines;

            // Try to find an existing parent bookmark titled "Appendix A"
            OutlineItemCollection parent = null;
            foreach (OutlineItemCollection item in rootOutlines)
            {
                if (item.Title == "Appendix A")
                {
                    parent = item;
                    break;
                }
            }

            // If the parent does not exist, create it and add to the root outlines
            if (parent == null)
            {
                parent = new OutlineItemCollection(rootOutlines)
                {
                    Title = "Appendix A",
                    Open = true // expanded by default, adjust as needed
                };
                rootOutlines.Add(parent);
            }

            // Create the child bookmark "Figure 1"
            OutlineItemCollection child = new OutlineItemCollection(rootOutlines)
            {
                Title = "Figure 1",
                Open = false // collapsed by default, adjust as needed
            };

            // Ensure the document has at least 23 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count >= 23)
            {
                // Set the destination of the child bookmark to page 23.
                // Using a FitExplicitDestination makes the page fill the view.
                child.Destination = new FitExplicitDestination(doc.Pages[23]);
            }
            else
            {
                Console.Error.WriteLine("The document does not contain page 23.");
                // Still add the bookmark without a valid destination to keep the flow.
            }

            // Attach the child bookmark under the parent
            parent.Add(child);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
