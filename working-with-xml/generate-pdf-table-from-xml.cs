using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath = "data.xml";
        const string pdfPath = "report.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML data
        XDocument xDoc = XDocument.Load(xmlPath);
        XElement? root = xDoc.Root;
        if (root == null)
        {
            Console.Error.WriteLine("Invalid XML: missing root element.");
            return;
        }

        // Expected XML format:
        // <Table>
        //   <Header><Column>Header1</Column><Column>Header2</Column>...</Header>
        //   <Row><Cell>Data11</Cell><Cell>Data12</Cell>...</Row>
        //   <Row>...</Row>
        // </Table>

        // Header may be missing, so make the list nullable
        List<XElement>? headerColumns = root.Element("Header")?.Elements("Column").ToList();
        List<XElement> dataRows = root.Elements("Row").ToList();

        // Determine column count (header preferred, otherwise first data row)
        int columnCount = headerColumns?.Count ??
                          dataRows.FirstOrDefault()?.Elements("Cell").Count() ??
                          0;

        if (columnCount == 0)
        {
            Console.Error.WriteLine("No column information found in XML.");
            return;
        }

        // Create PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a page
            Page page = pdfDoc.Pages.Add();

            // Create a table
            Table pdfTable = new Table
            {
                Border = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Add header row if present
            if (headerColumns != null && headerColumns.Any())
            {
                Row headerRow = pdfTable.Rows.Add();
                foreach (XElement col in headerColumns)
                {
                    Cell cell = headerRow.Cells.Add();
                    cell.BackgroundColor = Aspose.Pdf.Color.LightGray;
                    cell.Paragraphs.Add(new TextFragment(col.Value ?? string.Empty)
                    {
                        TextState = { FontSize = 12, FontStyle = FontStyles.Bold }
                    });
                }
            }

            // Add data rows
            foreach (XElement row in dataRows)
            {
                Row pdfRow = pdfTable.Rows.Add();
                foreach (XElement cellData in row.Elements("Cell"))
                {
                    Cell cell = pdfRow.Cells.Add();
                    cell.Paragraphs.Add(new TextFragment(cellData.Value ?? string.Empty));
                }
            }

            // Ensure each row has the expected number of cells (add empty cells if needed)
            foreach (Row r in pdfTable.Rows)
            {
                while (r.Cells.Count < columnCount)
                {
                    r.Cells.Add();
                }
            }

            // Set equal column widths based on page width
            double usableWidth = page.PageInfo.Width - page.PageInfo.Margin.Left - page.PageInfo.Margin.Right;
            float colWidth = (float)(usableWidth / columnCount);
            pdfTable.ColumnWidths = string.Join(" ", Enumerable.Repeat(colWidth.ToString(System.Globalization.CultureInfo.InvariantCulture), columnCount));

            // Add the table to the page
            page.Paragraphs.Add(pdfTable);

            // Save the PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF report generated: {pdfPath}");
    }
}
