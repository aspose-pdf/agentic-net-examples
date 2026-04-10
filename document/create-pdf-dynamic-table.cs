using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for BorderInfo and MarginInfo if needed

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a DataTable with dynamic number of rows.
        // ------------------------------------------------------------
        DataTable data = new DataTable();
        data.Columns.Add("ID", typeof(int));
        data.Columns.Add("Product", typeof(string));
        data.Columns.Add("Quantity", typeof(int));

        // Example: add 20 rows (could be any collection size)
        for (int i = 1; i <= 20; i++)
        {
            data.Rows.Add(i, $"Item {i}", i * 10);
        }

        // ------------------------------------------------------------
        // Create a new PDF document.
        // ------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a single page.
            Page page = doc.Pages.Add();

            // --------------------------------------------------------
            // Create a Table and configure its appearance.
            // --------------------------------------------------------
            Table table = new Table
            {
                // Define three column widths (in points).
                ColumnWidths = "100 200 100",

                // Set a thin black border for all cells.
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black),

                // Add some padding inside each cell.
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // --------------------------------------------------------
            // Import the DataTable into the Table.
            // Parameters:
            //   - data: source DataTable
            //   - true: import column names as the first row
            //   - 0: start importing at the first row of the table
            //   - 0: start importing at the first column of the table
            // --------------------------------------------------------
            table.ImportDataTable(data, true, 0, 0);

            // Add the populated table to the page.
            page.Paragraphs.Add(table);

            // ------------------------------------------------------------
            // Save the PDF document.
            // ------------------------------------------------------------
            doc.Save("DynamicTable.pdf");
        }

        Console.WriteLine("PDF with dynamic table created successfully.");
    }
}