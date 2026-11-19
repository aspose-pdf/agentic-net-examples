using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input XML containing tabular data
        const string xmlPath = "data.xml";
        // Output PDF file
        const string pdfPath = "report.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML
        XDocument xDoc = XDocument.Load(xmlPath);
        // Expecting structure: <Table><Row><Cell>value</Cell>...</Row>...</Table>
        XElement tableElement = xDoc.Root;
        if (tableElement == null || tableElement.Name != "Table")
        {
            Console.Error.WriteLine("Invalid XML format. Root element must be <Table>.");
            return;
        }

        // Determine number of columns from the first row
        int columnCount = 0;
        foreach (XElement row in tableElement.Elements("Row"))
        {
            columnCount = Math.Max(columnCount, row.Elements("Cell").Count());
            break; // only need first row for column count
        }
        if (columnCount == 0)
        {
            Console.Error.WriteLine("No data found in XML.");
            return;
        }

        // Create PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add a page
            Aspose.Pdf.Page page = doc.Pages.Add();

            // Create a table
            Aspose.Pdf.Table pdfTable = new Aspose.Pdf.Table
            {
                // Set column widths equally
                ColumnWidths = string.Join(" ", new string[columnCount].Select(_ => "100"))
            };

            // Define a default font
            Aspose.Pdf.Text.Font defaultFont = FontRepository.FindFont("Arial");

            // Optional: add a header row (first XML row can be treated as header)
            bool isFirstRow = true;
            foreach (XElement row in tableElement.Elements("Row"))
            {
                Aspose.Pdf.Row pdfRow = pdfTable.Rows.Add();

                int cellIndex = 0;
                foreach (XElement cell in row.Elements("Cell"))
                {
                    Aspose.Pdf.Cell pdfCell = pdfRow.Cells.Add();

                    // Create a text fragment for the cell content
                    Aspose.Pdf.Text.TextFragment tf = new Aspose.Pdf.Text.TextFragment(cell.Value)
                    {
                        // Apply font and size
                        TextState = { Font = defaultFont, FontSize = 12 }
                    };

                    // Header styling
                    if (isFirstRow)
                    {
                        tf.TextState.FontStyle = FontStyles.Bold;
                        pdfCell.BackgroundColor = Aspose.Pdf.Color.LightGray;
                    }

                    // Add the text fragment to the cell
                    pdfCell.Paragraphs.Add(tf);

                    // Optional: set cell border
                    pdfCell.Border = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, 0.5f, Aspose.Pdf.Color.Black);

                    cellIndex++;
                }

                // If the row has fewer cells than columns, add empty cells
                while (cellIndex < columnCount)
                {
                    Aspose.Pdf.Cell emptyCell = pdfRow.Cells.Add();
                    emptyCell.Border = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, 0.5f, Aspose.Pdf.Color.Black);
                    cellIndex++;
                }

                isFirstRow = false;
            }

            // Add the table to the page
            page.Paragraphs.Add(pdfTable);

            // Save the PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF report generated: {pdfPath}");
    }
}