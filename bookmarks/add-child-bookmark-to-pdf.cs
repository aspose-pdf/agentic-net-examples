using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // GoToAction lives here

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "child1.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Locate the parent bookmark titled "Chapter One"
            OutlineItemCollection parentBookmark = null;
            foreach (OutlineItemCollection item in doc.Outlines)
            {
                if (item.Title == "Chapter One")
                {
                    parentBookmark = item;
                    break;
                }
            }

            if (parentBookmark == null)
            {
                Console.Error.WriteLine("Parent bookmark 'Chapter One' not found.");
                return;
            }

            // Create a child bookmark under the parent
            OutlineItemCollection childBookmark = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Section 1.1",
                Action = new GoToAction(doc.Pages[6]) // page index is 1‑based
            };

            // Add the child to the parent outline collection
            parentBookmark.Add(childBookmark);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
