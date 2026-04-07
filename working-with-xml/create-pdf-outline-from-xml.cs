using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Correct namespace for GoToAction

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF
        const string inputXmlPath  = "sections.xml"; // XML with section titles
        const string outputPdfPath = "output_with_outline.pdf";

        // Verify files exist
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

        // Load the XML document containing section titles and page numbers
        XDocument xmlDoc = XDocument.Load(inputXmlPath);

        // Open the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Expected XML format:
            // <Sections>
            //   <Section title="Chapter 1" page="1" />
            //   <Section title="Chapter 2" page="5" />
            //   ...
            // </Sections>
            foreach (XElement section in xmlDoc.Root.Elements("Section"))
            {
                string title = (string)section.Attribute("title");
                int pageNumber = (int?)section.Attribute("page") ?? 1; // default to first page if missing

                // Ensure the page number is within the PDF page range
                if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {pageNumber} for title \"{title}\". Skipping.");
                    continue;
                }

                // Retrieve the target page (Aspose.Pdf uses 1‑based indexing)
                Page targetPage = pdfDoc.Pages[pageNumber];

                // Create a new outline item linked to the target page
                OutlineItemCollection outlineItem = new OutlineItemCollection(pdfDoc.Outlines)
                {
                    Title  = title,
                    Action = new GoToAction(targetPage) // navigation action
                };

                // Add the outline item to the document's outline collection
                pdfDoc.Outlines.Add(outlineItem);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with custom outlines saved to '{outputPdfPath}'.");
    }
}
