using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Prepare source data in a DataTable
        DataTable sourceTable = new DataTable();
        sourceTable.Columns.Add("ID", typeof(int));
        sourceTable.Columns.Add("Name", typeof(string));
        sourceTable.Columns.Add("Quantity", typeof(int));

        // Dynamically determine number of records (example: 5 rows)
        for (int i = 1; i <= 5; i++)
        {
            sourceTable.Rows.Add(i, $"Item {i}", i * 10);
        }

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a table and set basic appearance
            Table table = new Table
            {
                // Optional: set column widths (percentage of page width)
                ColumnWidths = "100 200 100",
                // Optional: set border for visual clarity
                Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black)
            };

            // Import the DataTable into the Aspose.Pdf.Table
            // Parameters: (DataTable, import column names, first row, first column)
            table.ImportDataTable(sourceTable, true, 0, 0);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save("DynamicTable.pdf");
        }

        Console.WriteLine("PDF with dynamic table created successfully.");
    }
}