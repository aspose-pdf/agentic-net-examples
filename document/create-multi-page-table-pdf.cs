using System;
using System.Data;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document (first page)
            Page page = doc.Pages.Add();

            // Create a table that will automatically break across pages
            Table table = new Table
            {
                // Define column widths (in points)
                ColumnWidths = "50 150 250",
                // Enable automatic breaking across pages
                IsBroken = true,
                // Repeat the first row (header) on each new page fragment
                RepeatingRowsCount = 1
            };

            // ----- Header row -----
            Row header = table.Rows.Add();
            header.Cells.Add("ID");
            header.Cells.Add("Name");
            header.Cells.Add("Description");

            // ----- Populate data rows -----
            DataTable data = new DataTable();
            data.Columns.Add("ID", typeof(int));
            data.Columns.Add("Name", typeof(string));
            data.Columns.Add("Description", typeof(string));

            // Generate enough rows to span multiple pages
            for (int i = 1; i <= 200; i++)
            {
                data.Rows.Add(i, $"Item {i}", $"Description for item {i}");
            }

            // Import the data rows into the table (skip column names because header already added)
            // Parameters: DataTable, isColumnNamesImported, firstFilledRow, firstFilledColumn
            table.ImportDataTable(data, false, 0, 0);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF (extension determines PDF output)
            doc.Save("MultiPageTable.pdf");
        }

        Console.WriteLine("PDF with multi‑page table created successfully.");
    }
}