using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for GoToAction

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF
        const string inputXmlPath  = "structure.xml"; // XML describing hierarchy
        const string outputPdfPath = "output_with_outline.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(inputXmlPath))
        {
            Console.Error.WriteLine($"XML not found: {inputXmlPath}");
            return;
        }

        // Load the XML that defines the outline hierarchy
        XDocument xmlDoc = XDocument.Load(inputXmlPath);
        XElement rootElement = xmlDoc.Root;
        if (rootElement == null)
        {
            Console.Error.WriteLine("XML does not contain a root element.");
            return;
        }

        // Open the PDF document inside a using block (ensures proper disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the document has an outline collection (it is always present)
            OutlineCollection outlines = pdfDoc.Outlines;

            // Page index is 1‑based in Aspose.Pdf
            int pageIndex = 1;
            int totalPages = pdfDoc.Pages.Count;

            // Recursively create outline items from the XML structure
            CreateOutlineFromXmlElement(rootElement, null, outlines, pdfDoc, ref pageIndex, totalPages);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with hierarchical outline: {outputPdfPath}");
    }

    /// <summary>
    /// Recursively creates outline items that mirror the XML element hierarchy.
    /// </summary>
    /// <param name="xmlElement">Current XML element.</param>
    /// <param name="parentOutline">Parent outline item; null for top‑level items.</param>
    /// <param name="outlinesRoot">The document's OutlineCollection (used for top‑level items).</param>
    /// <param name="doc">The PDF document.</param>
    /// <param name="pageIdx">Reference to the current page index (1‑based). It is incremented for each outline entry.</param>
    /// <param name="maxPages">Total number of pages in the PDF.</param>
    private static void CreateOutlineFromXmlElement(
        XElement xmlElement,
        OutlineItemCollection parentOutline,
        OutlineCollection outlinesRoot,
        Document doc,
        ref int pageIdx,
        int maxPages)
    {
        // Create a new outline item associated with the appropriate collection
        OutlineItemCollection outlineItem = parentOutline == null
            ? new OutlineItemCollection(outlinesRoot)   // top‑level
            : new OutlineItemCollection(outlinesRoot); // child items use same constructor

        // Set the title of the bookmark to the element name (you can customize this)
        outlineItem.Title = xmlElement.Name.LocalName;

        // Assign a destination/action to the outline item.
        // If we have enough pages, point to the current page; otherwise reuse the last page.
        int targetPage = pageIdx <= maxPages ? pageIdx : maxPages;
        outlineItem.Action = new GoToAction(doc.Pages[targetPage]);

        // Add the outline item to its parent or to the document's root collection
        if (parentOutline == null)
        {
            outlinesRoot.Add(outlineItem);
        }
        else
        {
            parentOutline.Add(outlineItem);
        }

        // Move to the next page for the following outline entry
        pageIdx++;

        // Process child XML elements recursively
        foreach (XElement child in xmlElement.Elements())
        {
            CreateOutlineFromXmlElement(child, outlineItem, outlinesRoot, doc, ref pageIdx, maxPages);
        }
    }
}