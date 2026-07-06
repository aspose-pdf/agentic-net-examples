using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position on the page
            Table table = new Table
            {
                // Position the table (left, top)
                Left = 50,
                Top = 750,
                // Define column widths (example: three columns)
                ColumnWidths = "100 200 100"
            };

            // ------------------------------
            // Define header rows (will be repeated)
            // ------------------------------

            // First header row
            Row headerRow1 = table.Rows.Add();
            headerRow1.Cells.Add("ID");
            headerRow1.Cells.Add("Description");
            headerRow1.Cells.Add("Amount");

            // Second header row (optional, demonstrates multiple header rows)
            Row headerRow2 = table.Rows.Add();
            headerRow2.Cells.Add("----");
            headerRow2.Cells.Add("------------");
            headerRow2.Cells.Add("------");

            // Set the number of rows to repeat on each new page
            // Here we have 2 header rows, so set RepeatingRowsCount to 2
            table.RepeatingRowsCount = 2;

            // ------------------------------
            // Add data rows (enough to span multiple pages)
            // ------------------------------

            // Create a DataTable with sample data
            DataTable data = new DataTable();
            data.Columns.Add("ID", typeof(int));
            data.Columns.Add("Description", typeof(string));
            data.Columns.Add("Amount", typeof(decimal));

            // Populate with many rows to force pagination
            for (int i = 1; i <= 200; i++)
            {
                data.Rows.Add(i, $"Item #{i}", i * 10.5m);
            }

            // Import the DataTable into the Aspose.Pdf.Table
            // The first row (index 0) is NOT a header because we already added headers manually
            table.ImportDataTable(data, false, 0, 0);

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save("TableWithRepeatingHeaders.pdf");
        }

        Console.WriteLine("PDF created with repeating table header rows.");
    }
}