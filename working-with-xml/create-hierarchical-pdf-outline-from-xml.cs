using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Recursively creates outline items from XML elements.
    static void AddOutlineItems(OutlineCollection outlineRoot, OutlineItemCollection parentItem, XElement xmlElement, Document pdfDoc, ref int pageIndex)
    {
        // Create a new outline entry for the current XML element.
        var outlineItem = new OutlineItemCollection(outlineRoot)
        {
            Title = xmlElement.Name.LocalName,
            // Assign a destination page (1‑based). If the PDF has fewer pages, the last page is used.
            Action = new GoToAction(pdfDoc.Pages[Math.Min(pageIndex, pdfDoc.Pages.Count)])
        };
        // Increment page index for the next element.
        pageIndex++;

        // Attach the new item to its parent (or to the root collection if parentItem is null).
        if (parentItem == null)
        {
            outlineRoot.Add(outlineItem);
        }
        else
        {
            parentItem.Add(outlineItem);
        }

        // Process child XML elements recursively.
        foreach (var child in xmlElement.Elements())
        {
            AddOutlineItems(outlineRoot, outlineItem, child, pdfDoc, ref pageIndex);
        }
    }

    static void Main()
    {
        const string pdfInputPath  = "input.pdf";   // Existing PDF to which the outline will be added
        const string xmlInputPath  = "structure.xml"; // XML describing the hierarchical structure
        const string pdfOutputPath = "output_with_outline.pdf";

        if (!File.Exists(pdfInputPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfInputPath}");
            return;
        }
        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlInputPath}");
            return;
        }

        // Load the XML that defines the hierarchy.
        XDocument xmlDoc = XDocument.Load(xmlInputPath);
        // Load the PDF document.
        using (Document pdfDoc = new Document(pdfInputPath))
        {
            // Ensure the document has an outline collection.
            OutlineCollection outlines = pdfDoc.Outlines;

            // Start page numbering at 1.
            int pageCounter = 1;

            // For each top‑level XML element, create a corresponding top‑level bookmark.
            foreach (var rootElement in xmlDoc.Root.Elements())
            {
                AddOutlineItems(outlines, null, rootElement, pdfDoc, ref pageCounter);
            }

            // Save the modified PDF.
            pdfDoc.Save(pdfOutputPath);
        }

        Console.WriteLine($"PDF with hierarchical outline saved to '{pdfOutputPath}'.");
    }
}