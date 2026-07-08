using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_margins.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page (first page is index 1)
            Page page = doc.Pages.Add();

            // Create a table with three equal-width columns
            Table table = new Table
            {
                ColumnWidths = "100 100 100"
            };
            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row with some text
            Cell cell = row.Cells.Add("Cell with custom margins");

            // Configure the cell's margins (left, bottom, right, top)
            cell.Margin = new MarginInfo(5, 5, 5, 5);

            // Save the PDF to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}