using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string xmlPath   = "tableDefinition.xml";
        const string pdfPath   = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML definition not found: {xmlPath}");
            return;
        }

        // Load the XML that defines the table structure
        XDocument xDoc = XDocument.Load(xmlPath);
        XElement tableElem = xDoc.Root; // <Table>

        // Create a new empty PDF document
        using (Document pdfDoc = new Document())
        {
            // Enable tagging and set basic metadata
            ITaggedContent tagged = pdfDoc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Table from XML");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create the table structure element
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Table generated from XML definition";
            root.AppendChild(table);

            // Process optional <Header> section
            XElement headerXml = tableElem.Element("Header");
            if (headerXml != null)
            {
                TableTHeadElement thead = tagged.CreateTableTHeadElement();
                table.AppendChild(thead);

                TableTRElement headerRow = tagged.CreateTableTRElement();
                thead.AppendChild(headerRow);

                foreach (XElement cellXml in headerXml.Elements("Cell"))
                {
                    TableTHElement th = tagged.CreateTableTHElement();
                    th.SetText(cellXml.Value.Trim());
                    headerRow.AppendChild(th);
                }
            }

            // Process <Row> elements (body)
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            foreach (XElement rowXml in tableElem.Elements("Row"))
            {
                TableTRElement bodyRow = tagged.CreateTableTRElement();
                tbody.AppendChild(bodyRow);

                foreach (XElement cellXml in rowXml.Elements("Cell"))
                {
                    TableTDElement td = tagged.CreateTableTDElement();
                    td.SetText(cellXml.Value.Trim());
                    bodyRow.AppendChild(td);
                }
            }

            // Save the PDF (no PreSave required)
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF with table saved to '{pdfPath}'.");
    }
}