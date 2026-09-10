using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for BorderInfo and BorderSide if needed

class Program
{
    static void Main()
    {
        // Build a DataTable dynamically – rows will be added based on a collection size
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("ID", typeof(int));
        dataTable.Columns.Add("Product", typeof(string));
        dataTable.Columns.Add("Quantity", typeof(int));

        // Example collection; replace with any data source
        var products = new[]
        {
            new { ID = 1, Product = "Apple",  Quantity = 10 },
            new { ID = 2, Product = "Banana", Quantity = 20 },
            new { ID = 3, Product = "Cherry", Quantity = 15 },
            // Add more items as needed – the table will expand automatically
        };

        foreach (var p in products)
        {
            dataTable.Rows.Add(p.ID, p.Product, p.Quantity);
        }

        // Create a new PDF document (lifecycle managed by using)
        using (Document doc = new Document())
        {
            // Add a page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a table and configure basic appearance
            Table table = new Table
            {
                // Define three column widths (in points)
                ColumnWidths = "80 200 80",
                // Optional: set a thin black border for all cells
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
            };

            // Import the DataTable into the Aspose.Pdf Table.
            // Parameters: (DataTable, importColumnNames, firstRow, firstColumn)
            table.ImportDataTable(dataTable, true, 0, 0);

            // Add the populated table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save("DynamicTable.pdf");
        }
    }
}