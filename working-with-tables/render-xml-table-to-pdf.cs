using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input XML that defines the table structure
        const string xmlPath = "tableDefinition.xml";
        // Output PDF file
        const string pdfPath = "tableOutput.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML definition not found: {xmlPath}");
            return;
        }

        // Load the XML document
        XDocument xDoc = XDocument.Load(xmlPath);
        XElement root = xDoc.Root; // Expected <Table> root element
        if (root == null)
        {
            Console.Error.WriteLine("Invalid XML: missing root element.");
            return;
        }

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a blank page (Pages are 1‑based)
            Page page = pdfDoc.Pages.Add();

            // Create an Aspose.Pdf.Table instance
            Table table = new Table();

            // Determine column count from the first <Row> element
            int columnCount = 0;
            XElement firstRow = root.Element("Row");
            if (firstRow != null)
            {
                columnCount = firstRow.Elements("Cell").Count();
                if (columnCount > 0)
                {
                    // Set equal column widths (optional: adjust as needed)
                    double[] columnWidths = new double[columnCount];
                    for (int i = 0; i < columnCount; i++)
                        columnWidths[i] = 1.0 / columnCount;
                    // ColumnWidths property expects a comma‑separated string
                    table.ColumnWidths = string.Join(",", columnWidths.Select(w => w.ToString(System.Globalization.CultureInfo.InvariantCulture)));
                }
            }

            // Iterate over each <Row> element in the XML
            foreach (XElement rowElem in root.Elements("Row"))
            {
                // Create a new row in the PDF table
                Row pdfRow = table.Rows.Add();

                // Iterate over each <Cell> within the current row
                foreach (XElement cellElem in rowElem.Elements("Cell"))
                {
                    // Create a new cell in the PDF table
                    Cell pdfCell = pdfRow.Cells.Add();

                    // Extract the cell text (trim to avoid whitespace issues)
                    string cellText = (cellElem.Value ?? string.Empty).Trim();

                    // Add the text to the cell using a TextFragment
                    TextFragment tf = new TextFragment(cellText);
                    pdfCell.Paragraphs.Add(tf);
                }
            }

            // Add the constructed table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF with table saved to '{pdfPath}'.");
    }
}
