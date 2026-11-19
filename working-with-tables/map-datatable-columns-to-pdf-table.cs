using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Prepare a sample DataTable
        DataTable dt = new DataTable();
        dt.Columns.Add("FirstName", typeof(string));
        dt.Columns.Add("LastName", typeof(string));
        dt.Columns.Add("Age", typeof(int));

        dt.Rows.Add("John", "Doe", 30);
        dt.Rows.Add("Jane", "Smith", 28);
        dt.Rows.Add("Bob", "Johnson", 45);

        // Define mapping: DataTable column name -> Table column name
        // The Table will have columns "Name", "Surname", "Years"
        var columnMapping = new Dictionary<string, string>
        {
            { "FirstName", "Name" },
            { "LastName",  "Surname" },
            { "Age",       "Years" }
        };

        // Reverse mapping to get source column by target column name
        var reverseMapping = columnMapping.ToDictionary(kv => kv.Value, kv => kv.Key);
        // Define the order of target columns as they should appear in the PDF table
        var targetColumnOrder = new List<string> { "Name", "Surname", "Years" };

        // Output PDF path
        const string outputPath = "MappedTable.pdf";

        // Create PDF document and add a page
        using (Document pdfDoc = new Document())
        {
            Page page = pdfDoc.Pages.Add();

            // Create a Table with three columns (matching the mapped column names)
            Table table = new Table
            {
                ColumnWidths = "150 150 100", // adjust widths as needed
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Add a header row manually (optional but improves readability)
            Row header = table.Rows.Add();
            header.Cells.Add("Name");
            header.Cells.Add("Surname");
            header.Cells.Add("Years");
            // Apply simple styling to header cells
            foreach (Cell cell in header.Cells)
            {
                cell.BackgroundColor = Color.LightGray;
                cell.DefaultCellTextState = new TextState { FontSize = 12, FontStyle = FontStyles.Bold };
            }

            // Manually import DataTable rows using the column mapping.
            foreach (DataRow dr in dt.Rows)
            {
                Row row = table.Rows.Add();
                foreach (string targetCol in targetColumnOrder)
                {
                    string sourceCol = reverseMapping[targetCol];
                    object value = dr[sourceCol];
                    row.Cells.Add(value?.ToString() ?? string.Empty);
                }
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF with mapped table saved to '{outputPath}'.");
    }
}
