using System;
using System.Data;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Prepare in‑memory data
        DataTable dt = new DataTable("Sample");
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Price", typeof(decimal));

        dt.Rows.Add(1, "Apple", 0.99m);
        dt.Rows.Add(2, "Banana", 0.59m);
        dt.Rows.Add(3, "Cherry", 2.49m);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and define column widths (optional)
            Table table = new Table
            {
                // Three columns of equal width
                ColumnWidths = "100 100 100"
            };

            // Import the DataTable into the Aspose.Pdf Table
            // Parameters: (DataTable source, bool hasHeader, int startRow, int startColumn)
            table.ImportDataTable(dt, true, 0, 0);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save("DataTable_Export.pdf");
        }

        Console.WriteLine("PDF with imported DataTable has been saved as 'DataTable_Export.pdf'.");
    }
}