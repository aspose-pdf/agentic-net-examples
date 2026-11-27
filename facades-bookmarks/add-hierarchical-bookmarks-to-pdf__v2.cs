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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the core Document API (preferred for bookmark manipulation)
        Document doc = new Document(inputPath);

        // Create the parent bookmark (Chapter 3) on page 10
        OutlineItemCollection parent = new OutlineItemCollection(doc.Outlines);
        parent.Title = "Chapter 3";
        // Destination: XYZExplicitDestination(page, left, top, zoom). Using default zoom (1) and top‑left (0,0).
        parent.Destination = new XYZExplicitDestination(doc.Pages[10], 0, 0, 1);

        // Create child bookmark Section 3.1 on page 11
        OutlineItemCollection child1 = new OutlineItemCollection(doc.Outlines);
        child1.Title = "Section 3.1";
        child1.Destination = new XYZExplicitDestination(doc.Pages[11], 0, 0, 1);
        parent.Add(child1); // add to parent

        // Create child bookmark Section 3.2 on page 12
        OutlineItemCollection child2 = new OutlineItemCollection(doc.Outlines);
        child2.Title = "Section 3.2";
        child2.Destination = new XYZExplicitDestination(doc.Pages[12], 0, 0, 1);
        parent.Add(child2); // add to parent

        // Add the top‑level bookmark to the document outline collection
        doc.Outlines.Add(parent);

        // Save the updated PDF
        doc.Save(outputPath);

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
