using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfInputPath  = "input.pdf";
        const string pdfOutputPath = "output_with_bookmarks.pdf";
        const string xmlInputPath  = "structure.xml";

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
        XElement rootElement = xmlDoc.Root;
        if (rootElement == null)
        {
            Console.Error.WriteLine("XML does not contain a root element.");
            return;
        }

        // Open the PDF document.
        using (Document pdfDoc = new Document(pdfInputPath))
        {
            // Remove any existing outlines.
            pdfDoc.Outlines.Delete();

            // Build bookmarks based on XML nesting.
            BuildBookmarksRecursive(rootElement, null, pdfDoc);

            // Save the modified PDF.
            pdfDoc.Save(pdfOutputPath);
        }

        Console.WriteLine($"PDF saved with hierarchical bookmarks to '{pdfOutputPath}'.");
    }

    /// <summary>
    /// Recursively creates outline items that mirror the XML element hierarchy.
    /// </summary>
    /// <param name="xmlNode">Current XML element.</param>
    /// <param name="parentOutline">Parent outline item; null for top‑level items.</param>
    /// <param name="pdfDoc">The PDF document being modified.</param>
    private static void BuildBookmarksRecursive(XElement xmlNode, OutlineItemCollection parentOutline, Document pdfDoc)
    {
        // Create a new outline item. The constructor requires the root OutlineCollection.
        OutlineItemCollection outlineItem = new OutlineItemCollection(pdfDoc.Outlines)
        {
            // Use the element name as the bookmark title. Adjust as needed.
            Title = xmlNode.Name.LocalName
        };

        // Example action: navigate to the first page of the PDF.
        // Adjust the destination logic as required for your scenario.
        outlineItem.Action = new GoToAction(pdfDoc.Pages[1]);

        // Attach the new outline item to its parent or to the document's root collection.
        if (parentOutline == null)
        {
            // Top‑level bookmark.
            pdfDoc.Outlines.Add(outlineItem);
        }
        else
        {
            // Child bookmark.
            parentOutline.Add(outlineItem);
        }

        // Recurse for each child XML element, increasing the hierarchy depth.
        foreach (XElement child in xmlNode.Elements())
        {
            BuildBookmarksRecursive(child, outlineItem, pdfDoc);
        }
    }
}