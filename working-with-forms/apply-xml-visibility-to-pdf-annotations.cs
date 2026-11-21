using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string xmlPath   = "visibility.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load visibility settings from XML.
        // Expected format:
        // <Visibility>
        //   <Section id="1" visible="true" />
        //   <Section id="2" visible="false" />
        //   ...
        // </Visibility>
        XDocument xDoc = XDocument.Load(xmlPath);
        var sections = xDoc.Root?.Elements("Section");

        if (sections == null)
        {
            Console.Error.WriteLine("No <Section> elements found in XML.");
            return;
        }

        // Collect page numbers that must be removed (invisible).
        var pagesToRemove = new System.Collections.Generic.List<int>();

        foreach (var sec in sections)
        {
            XAttribute idAttr = sec.Attribute("id");
            XAttribute visAttr = sec.Attribute("visible");

            if (idAttr == null || visAttr == null)
                continue; // skip malformed entries

            if (!int.TryParse(idAttr.Value, out int pageId))
                continue; // invalid page number

            if (!bool.TryParse(visAttr.Value, out bool isVisible))
                continue; // invalid visibility flag

            if (!isVisible)
                pagesToRemove.Add(pageId);
        }

        // Apply visibility: remove invisible pages.
        // Deleting pages from highest index to lowest prevents reindexing issues.
        pagesToRemove.Sort();
        pagesToRemove.Reverse();

        using (Document doc = new Document(pdfPath))
        {
            foreach (int pageNum in pagesToRemove)
            {
                // Ensure the page number is within the current range.
                if (pageNum >= 1 && pageNum <= doc.Pages.Count)
                {
                    doc.Pages.Delete(pageNum);
                }
            }

            doc.Save(outputPdf);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPdf}'.");
    }
}