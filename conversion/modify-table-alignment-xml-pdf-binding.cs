using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the XML structure file, and the output PDF
        const string pdfPath = "input.pdf";
        const string xmlPath = "structure.xml";
        const string outputPdf = "output.pdf";

        // Verify that the required files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document that defines the logical structure
        XDocument xDoc = XDocument.Load(xmlPath);

        // Identify the Table element by its Id attribute (adjust the Id value as needed)
        const string targetTableId = "tbl1";
        var tableElement = xDoc.Descendants("Table")
                               .FirstOrDefault(e => (string)e.Attribute("Id") == targetTableId);

        if (tableElement == null)
        {
            Console.Error.WriteLine($"Table with Id '{targetTableId}' not found in XML.");
            return;
        }

        // Modify (or add) the Alignment attribute of the found Table element
        tableElement.SetAttributeValue("Alignment", "Center"); // possible values: Left, Center, Right, etc.

        // Save the modified XML into a memory stream for binding
        using (MemoryStream xmlStream = new MemoryStream())
        {
            xDoc.Save(xmlStream);
            xmlStream.Position = 0; // reset stream position before reading

            // Load the PDF document and bind the updated XML structure
            using (Document pdfDoc = new Document(pdfPath))
            {
                // BindXml accepts a stream containing the XML definition
                pdfDoc.BindXml(xmlStream);

                // Save the resulting PDF with the updated structure
                pdfDoc.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF saved successfully to '{outputPdf}'.");
    }
}