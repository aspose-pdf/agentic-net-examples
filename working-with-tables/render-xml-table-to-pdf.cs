using System;
using System.IO;
using System.Data;
using System.Linq;
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

        // Parse the XML file into a DataTable.
        DataTable dataTable = new DataTable();

        try
        {
            XDocument xDoc = XDocument.Load(xmlPath);
            // Expected format:
            // <Table>
            //   <Row><Cell>...</Cell><Cell>...</Cell>...</Row>
            //   <Row>...</Row>
            // </Table>

            var rowElements = xDoc.Root?.Elements("Row");
            if (rowElements == null || !rowElements.Any())
                throw new Exception("No <Row> elements found in XML.");

            // Determine the maximum number of cells in any row.
            int maxColumns = rowElements.Max(r => r.Elements("Cell").Count());

            // Create columns.
            for (int i = 0; i < maxColumns; i++)
                dataTable.Columns.Add($"Column{i + 1}", typeof(string));

            // Fill rows.
            foreach (var rowElem in rowElements)
            {
                DataRow dr = dataTable.NewRow();
                int colIndex = 0;
                foreach (var cellElem in rowElem.Elements("Cell"))
                {
                    dr[colIndex++] = cellElem.Value;
                }
                dataTable.Rows.Add(dr);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error parsing XML: {ex.Message}");
            return;
        }

        // Create a PDF document and render the table.
        using (Document pdfDoc = new Document())
        {
            // Add a page to the document.
            Page page = pdfDoc.Pages.Add();

            // Create a Table instance.
            Table table = new Table
            {
                // Set column widths proportionally (example: 100 units each).
                ColumnWidths = string.Join(" ", Enumerable.Repeat("100", dataTable.Columns.Count)),
                // Add a simple border to all cells.
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
            };

            // Import the DataTable into the Aspose.Pdf.Table.
            // Parameters: (DataTable, includeColumnNames, firstFilledRow, firstFilledColumn)
            table.ImportDataTable(dataTable, true, 0, 0);

            // Add the table to the page.
            page.Paragraphs.Add(table);

            // Save the PDF.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with rendered table saved to '{outputPdf}'.");
    }
}