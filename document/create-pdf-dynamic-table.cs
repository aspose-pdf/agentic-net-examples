using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "dynamic_table.pdf";

        // Create a sample DataTable with dynamic rows
        DataTable data = new DataTable();
        data.Columns.Add("ID", typeof(int));
        data.Columns.Add("Name", typeof(string));
        data.Columns.Add("Quantity", typeof(int));

        // Populate the DataTable (the number of rows can vary)
        for (int i = 1; i <= 15; i++) // example: 15 rows
        {
            data.Rows.Add(i, $"Item {i}", i * 10);
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Table instance
            Table table = new Table
            {
                // Optional styling
                Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
                // ColumnWidths expects a space‑separated string, not a float array
                ColumnWidths = "50 250 100"
            };

            // Import the DataTable into the Aspose.Pdf.Table
            // Parameters: (DataTable, import column names as first row, start row, start column)
            table.ImportDataTable(data, true, 0, 0);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dynamic table saved to '{outputPath}'.");
    }
}
