using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for XYZExplicitDestination

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "chapter3_child.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Find the bookmark (outline item) titled "Chapter Three"
            OutlineItemCollection chapterItem = null;
            foreach (OutlineItemCollection item in doc.Outlines)
            {
                if (item.Title == "Chapter Three")
                {
                    chapterItem = item;
                    break;
                }
            }

            if (chapterItem == null)
            {
                Console.Error.WriteLine("Bookmark 'Chapter Three' not found.");
                return;
            }

            // Create a new child bookmark for "Section 3.1"
            OutlineItemCollection sectionItem = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Section 3.1",
                // Destination points to page 61 (pages are 1‑based in Aspose.Pdf)
                Destination = new XYZExplicitDestination(doc.Pages[61], 0, 0, 1),
                Open = true // make the node initially expanded in the outline tree
            };

            // Append the new section as a child of the chapter bookmark
            chapterItem.Add(sectionItem);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved modified PDF to '{outputPath}'.");
    }
}
