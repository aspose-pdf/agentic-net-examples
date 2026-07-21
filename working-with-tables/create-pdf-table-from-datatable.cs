using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // ---------- 1. Create an in‑memory DataTable with sample data ----------
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Id", typeof(int));
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Quantity", typeof(int));
        dataTable.Columns.Add("Price", typeof(decimal));

        dataTable.Rows.Add(1, "Apple", 10, 0.5m);
        dataTable.Rows.Add(2, "Banana", 20, 0.3m);
        dataTable.Rows.Add(3, "Orange", 15, 0.4m);

        // ---------- 2. Create a new PDF document (lifecycle managed by using) ----------
        using (Document pdfDoc = new Document())
        {
            // Add a blank page (pages are 1‑based).
            Page page = pdfDoc.Pages.Add();

            // ---------- 3. Build a table and import the DataTable ----------
            Table table = new Table
            {
                Border = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Define column widths before importing the DataTable.
            // Aspose.Pdf requires column widths to be set; otherwise ImportDataTable throws a NullReferenceException.
            float[] columnWidths = new float[dataTable.Columns.Count];
            for (int i = 0; i < columnWidths.Length; i++)
                columnWidths[i] = 100; // equal width for each column (adjust as needed)

            // Table.ColumnWidths is a string property – provide a comma‑separated list.
            table.ColumnWidths = string.Join(",", columnWidths);

            // Import the DataTable. Parameters: (DataTable, import column names, first row, first column)
            table.ImportDataTable(dataTable, true, 0, 0);

            // Add the table to the page.
            page.Paragraphs.Add(table);

            // ---------- 4. Save the PDF ----------
            pdfDoc.Save("ProductsReport.pdf");
        }

        Console.WriteLine("PDF generated successfully: ProductsReport.pdf");
    }
}
