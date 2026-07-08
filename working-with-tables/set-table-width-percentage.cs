using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a table instance
            Table table = new Table();

            // Set the table width to 80% of the page width.
            // ColumnWidths accepts a string with percentage values.
            // With a single column, "80%" makes the whole table 80% wide.
            table.ColumnWidths = "80%";

            // Optional: position the table on the page (left/top coordinates)
            table.Left = 0;   // start at the left margin
            table.Top = 0;    // start at the top margin

            // Add a simple row and cell with some text for demonstration
            Row row = table.Rows.Add();
            Cell cell = row.Cells.Add();
            cell.Paragraphs.Add(new TextFragment("Sample cell"));

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}