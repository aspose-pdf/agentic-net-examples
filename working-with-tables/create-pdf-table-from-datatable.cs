using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create an in‑memory DataTable that mimics the result of a database query
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Column1", typeof(string));
        dataTable.Columns.Add("Column2", typeof(string));
        dataTable.Columns.Add("Column3", typeof(string));

        // Sample rows – replace or extend with your own data
        dataTable.Rows.Add("Row 1 – A", "Row 1 – B", "Row 1 – C");
        dataTable.Rows.Add("Row 2 – A", "Row 2 – B", "Row 2 – C");
        dataTable.Rows.Add("Row 3 – A", "Row 3 – B", "Row 3 – C");

        // Create a new PDF document
        using (Document pdfDocument = new Document())
        {
            // Add a page to the document
            Page page = pdfDocument.Pages.Add();

            // Create a table and configure its appearance
            Table table = new Table
            {
                // Adjust column widths as needed
                ColumnWidths = "100 150 200",
                Border = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Import the DataTable into the Aspose.Pdf.Table
            // Parameters: (DataTable, includeColumnHeaders, startRow, startColumn)
            table.ImportDataTable(dataTable, true, 0, 0);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF to disk
            pdfDocument.Save("output.pdf");
        }

        Console.WriteLine("PDF with imported table has been generated.");
    }
}
