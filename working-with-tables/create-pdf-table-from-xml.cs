using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath = "tableDefinition.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML that defines the table structure
        XDocument xDoc = XDocument.Load(xmlPath);
        XElement tableElement = xDoc.Root; // Expected <Table> root element

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a page to the document
            Page page = pdfDoc.Pages.Add();

            // Create an Aspose.Pdf Table
            Table pdfTable = new Table
            {
                // Optional visual styling
                Border = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Build rows and cells from the XML definition
            foreach (XElement rowElem in tableElement.Elements("Row"))
            {
                Row pdfRow = pdfTable.Rows.Add();

                foreach (XElement cellElem in rowElem.Elements("Cell"))
                {
                    Cell pdfCell = pdfRow.Cells.Add();

                    // Add the cell text
                    TextFragment tf = new TextFragment(cellElem.Value);
                    pdfCell.Paragraphs.Add(tf);
                }
            }

            // Add the constructed table to the page
            page.Paragraphs.Add(pdfTable);

            // Save the PDF document
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated successfully: {outputPdf}");
    }
}