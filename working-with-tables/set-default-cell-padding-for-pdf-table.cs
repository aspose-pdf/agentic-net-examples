using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "table_padding.pdf";

        // Document must be disposed via using (rule: document-disposal-with-using)
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a table with three equal-width columns
            Table table = new Table();
            table.ColumnWidths = "100 100 100";

            // Define default cell padding for the entire table
            MarginInfo padding = new MarginInfo();
            padding.Left   = 5;   // left padding
            padding.Right  = 5;   // right padding
            padding.Top    = 3;   // top padding
            padding.Bottom = 3;   // bottom padding
            table.DefaultCellPadding = padding; // apply to all cells

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add some data rows
            for (int i = 1; i <= 5; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Row {i} - Col 1");
                row.Cells.Add($"Row {i} - Col 2");
                row.Cells.Add($"Row {i} - Col 3");
            }

            // Place the table on the page
            page.Paragraphs.Add(table);

            // Save the PDF (rule: save-to-non-pdf-always-use-save-options not needed for PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table padding saved to '{outputPath}'.");
    }
}