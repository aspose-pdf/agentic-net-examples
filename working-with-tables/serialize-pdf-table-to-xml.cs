using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "table.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Verify that the PDF is tagged by checking the TaggedContent property
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("PDF is not tagged. No structure tree to extract tables.");
                return;
            }

            ITaggedContent tagged = doc.TaggedContent;

            // Find all TableElement objects in the structure tree (recursive search)
            var tables = tagged.RootElement.FindElements<TableElement>(true);

            // Build an XML document that will hold the serialized tables
            XElement rootXml = new XElement("Tables");

            foreach (TableElement table in tables)
            {
                XElement tableXml = new XElement("Table");

                // Optional description of the table – guard against null
                if (!string.IsNullOrEmpty(table.AlternativeText))
                    tableXml.SetAttributeValue("Description", table.AlternativeText ?? string.Empty);

                // Process each section of the table (thead, tbody, tfoot)
                foreach (Element child in table.ChildElements)
                {
                    if (child is TableTHeadElement thead)
                        ProcessTableSection(thead, "Header", tableXml);
                    else if (child is TableTBodyElement tbody)
                        ProcessTableSection(tbody, "Body", tableXml);
                    else if (child is TableTFootElement tfoot)
                        ProcessTableSection(tfoot, "Footer", tableXml);
                }

                rootXml.Add(tableXml);
            }

            // Save the constructed XML to a file
            XDocument xdoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), rootXml);
            xdoc.Save(outputXml);
            Console.WriteLine($"Table data serialized to XML: {outputXml}");
        }
    }

    // Helper method to convert a table section (thead, tbody, tfoot) into XML
    private static void ProcessTableSection(Element section, string sectionName, XElement tableXml)
    {
        // Each section contains TableTRElement rows
        foreach (Element rowElem in section.ChildElements)
        {
            if (rowElem is TableTRElement row)
            {
                XElement rowXml = new XElement("Row");

                // Convert each cell in the row
                foreach (Element cellElem in row.ChildElements)
                {
                    if (cellElem is TableTHElement th)
                    {
                        XElement cellXml = new XElement("Cell",
                            new XAttribute("Type", "Header"),
                            th.ActualText ?? string.Empty);
                        rowXml.Add(cellXml);
                    }
                    else if (cellElem is TableTDElement td)
                    {
                        XElement cellXml = new XElement("Cell",
                            new XAttribute("Type", "Data"),
                            td.ActualText ?? string.Empty);
                        rowXml.Add(cellXml);
                    }
                }

                // Ensure the section element exists in the table XML
                XElement? sectionXml = tableXml.Element(sectionName);
                if (sectionXml == null)
                {
                    sectionXml = new XElement(sectionName);
                    tableXml.Add(sectionXml);
                }

                sectionXml.Add(rowXml);
            }
        }
    }
}