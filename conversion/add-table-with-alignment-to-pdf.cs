using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source XML, the modified XML, and the output PDF
        const string sourceXmlPath   = "template.xml";
        const string modifiedXmlPath = "template_modified.xml";
        const string outputPdfPath   = "result.pdf";

        // Ensure the source XML exists
        if (!File.Exists(sourceXmlPath))
        {
            Console.Error.WriteLine($"Source XML not found: {sourceXmlPath}");
            return;
        }

        // Load the original XML document
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(sourceXmlPath);

        // Create a new <Table> element
        XmlElement tableElem = xmlDoc.CreateElement("Table");

        // Set a custom alignment attribute (e.g., Center, Left, Right)
        // The attribute name is "Alignment" as defined by Aspose.Pdf XML schema
        tableElem.SetAttribute("Alignment", "Center");

        // OPTIONAL: Add a header row with two columns
        XmlElement thead = xmlDoc.CreateElement("TableHeader");
        XmlElement headerRow = xmlDoc.CreateElement("TableRow");

        XmlElement th1 = xmlDoc.CreateElement("TableHeaderCell");
        th1.InnerText = "Column 1";
        headerRow.AppendChild(th1);

        XmlElement th2 = xmlDoc.CreateElement("TableHeaderCell");
        th2.InnerText = "Column 2";
        headerRow.AppendChild(th2);

        thead.AppendChild(headerRow);
        tableElem.AppendChild(thead);

        // OPTIONAL: Add a body with one data row
        XmlElement tbody = xmlDoc.CreateElement("TableBody");
        XmlElement dataRow = xmlDoc.CreateElement("TableRow");

        XmlElement td1 = xmlDoc.CreateElement("TableCell");
        td1.InnerText = "Value A";
        dataRow.AppendChild(td1);

        XmlElement td2 = xmlDoc.CreateElement("TableCell");
        td2.InnerText = "Value B";
        dataRow.AppendChild(td2);

        tbody.AppendChild(dataRow);
        tableElem.AppendChild(tbody);

        // Append the new table to the root element of the XML
        XmlElement root = xmlDoc.DocumentElement;
        root.AppendChild(tableElem);

        // Save the modified XML to a temporary file
        xmlDoc.Save(modifiedXmlPath);

        // Bind the modified XML to a PDF document and save
        // Document is wrapped in a using block for deterministic disposal
        using (Document pdfDoc = new Document())
        {
            // Bind the XML; this creates PDF pages based on the XML structure
            pdfDoc.BindXml(modifiedXmlPath);

            // Save the resulting PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF generated with custom table: {outputPdfPath}");
    }
}