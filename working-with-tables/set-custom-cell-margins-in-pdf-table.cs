using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for text handling if needed

class Program
{
    static void Main()
    {
        const string outputPath = "cell_margin.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a table with a single column of width 200 points
            Table table = new Table();
            table.ColumnWidths = "200";

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell with some sample text
            Cell cell = row.Cells.Add("Cell with custom margins");

            // Configure the cell's margins (left, bottom, right, top) in points
            cell.Margin = new MarginInfo(10, 5, 10, 5);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}