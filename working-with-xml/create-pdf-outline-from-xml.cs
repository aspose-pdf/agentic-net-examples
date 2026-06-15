using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for explicit destination classes

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath  = "input.pdf";
        const string xmlPath       = "sections.xml";
        const string outputPdfPath = "output_with_outline.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load the XML that contains section titles and target page numbers.
        // Expected format:
        // <document>
        //   <section title="Chapter 1" page="1" />
        //   <section title="Chapter 2" page="5" />
        //   ...
        // </document>
        XDocument xDoc = XDocument.Load(xmlPath);

        // Load the existing PDF document.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Access the outline (bookmark) collection of the PDF.
            OutlineCollection outlines = pdfDoc.Outlines;

            // Iterate over each <section> element in the XML.
            foreach (XElement sec in xDoc.Descendants("section"))
            {
                string title   = (string)sec.Attribute("title");
                int    pageNum = (int?)sec.Attribute("page") ?? 1; // default to first page if missing

                // Ensure the page number is within the document range.
                if (pageNum < 1 || pageNum > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {pageNum} for title \"{title}\" – skipping.");
                    continue;
                }

                // Create a new outline item linked to the root outline collection.
                OutlineItemCollection outlineItem = new OutlineItemCollection(outlines)
                {
                    Title = title,
                    // Use an explicit destination that fits the whole page.
                    Destination = new FitExplicitDestination(pdfDoc.Pages[pageNum])
                };

                // Add the outline item to the document's outline hierarchy.
                outlines.Add(outlineItem);
            }

            // Save the modified PDF with the new outline entries.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with outlines: {outputPdfPath}");
    }
}