using System;
using System.Data;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create an in‑memory DataTable with sample data (replaces the unavailable SqlClient code)
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Column1", typeof(string));
        dataTable.Columns.Add("Column2", typeof(int));
        dataTable.Columns.Add("Column3", typeof(DateTime));

        dataTable.Rows.Add("Row A", 10, DateTime.Today);
        dataTable.Rows.Add("Row B", 20, DateTime.Today.AddDays(1));
        dataTable.Rows.Add("Row C", 30, DateTime.Today.AddDays(2));

        // Create PDF and import the DataTable as a table
        using (Document pdfDoc = new Document())
        {
            // Add a new page (pages are 1‑based)
            Page page = pdfDoc.Pages.Add();

            // Create a PDF table and import the DataTable
            Table pdfTable = new Table();
            // Optional: set column widths (adjust as needed)
            pdfTable.ColumnWidths = "100 100 100";

            // Import data: include header row, start at first row/column
            pdfTable.ImportDataTable(dataTable, true, 0, 0);

            // Add the table to the page
            page.Paragraphs.Add(pdfTable);

            // Save the PDF
            pdfDoc.Save("output.pdf");
        }

        Console.WriteLine("PDF generated successfully.");
    }
}
