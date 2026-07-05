using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "right_aligned_table.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a table instance
            Table table = new Table();

            // Align the table to the right margin
            table.Alignment = HorizontalAlignment.Right;

            // Adjust the left coordinate if needed (example value)
            table.Left = 300; // modify based on page width and desired margin

            // Define column widths (two columns, each 200 units wide)
            table.ColumnWidths = "200 200";

            // Add first row with two cells
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Cell 1");
            row1.Cells.Add("Cell 2");

            // Add second row with two cells
            Row row2 = table.Rows.Add();
            row2.Cells.Add("Cell 3");
            row2.Cells.Add("Cell 4");

            // Insert the table into the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to {outputPath}");
    }
}