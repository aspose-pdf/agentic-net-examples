using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to merge (order matters)
        string[] inputFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        string outputFile = "merged.pdf";

        if (inputFiles.Length == 0)
        {
            Console.Error.WriteLine("No input files specified.");
            return;
        }

        // Verify the first file exists – it will be the base document
        if (!File.Exists(inputFiles[0]))
        {
            Console.Error.WriteLine($"File not found: {inputFiles[0]}");
            return;
        }

        // Use the first PDF as the target document (as per the merge rule)
        using (Document target = new Document(inputFiles[0]))
        {
            // Iterate over the remaining PDFs and merge them
            for (int i = 1; i < inputFiles.Length; i++)
            {
                string srcPath = inputFiles[i];
                if (!File.Exists(srcPath))
                {
                    Console.Error.WriteLine($"Skipping missing file: {srcPath}");
                    continue;
                }

                // Load each source PDF in its own using block (lifecycle rule)
                using (Document source = new Document(srcPath))
                {
                    // Merge pages
                    target.Pages.Add(source.Pages);

                    // Preserve bookmarks/outlines hierarchy by copying each outline item
                    foreach (OutlineItemCollection srcOutline in source.Outlines)
                    {
                        OutlineItemCollection cloned = CloneOutline(srcOutline, target);
                        target.Outlines.Add(cloned);
                    }
                }
            }

            // Save the merged PDF (still inside the using block)
            target.Save(outputFile);
        }

        Console.WriteLine($"Merged PDF saved to '{outputFile}'.");
    }

    // Recursively clones an outline item (bookmark) and its children for the target document
    private static OutlineItemCollection CloneOutline(OutlineItemCollection sourceItem, Document targetDoc)
    {
        // Create a new outline item attached to the target document's outline collection
        OutlineItemCollection copy = new OutlineItemCollection(targetDoc.Outlines)
        {
            Title = sourceItem.Title,
            Open = sourceItem.Open,
            Color = sourceItem.Color,
            Italic = sourceItem.Italic,
            Bold = sourceItem.Bold,
            Action = sourceItem.Action // preserve navigation action if present
        };

        // Recursively copy child outline items
        foreach (OutlineItemCollection child in sourceItem)
        {
            copy.Add(CloneOutline(child, targetDoc));
        }

        return copy;
    }
}