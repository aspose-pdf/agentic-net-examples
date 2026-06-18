using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF (self‑contained example)
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // Open the sample PDF and add a table with a background color
        using (Document doc = new Document("input.pdf"))
        {
            // Page indexing is 1‑based
            Page page = doc.Pages[1];

            // Create a table
            Table table = new Table();
            table.ColumnWidths = "100 100 100";
            // The core API does not support gradient brushes for Table.BackgroundColor.
            // Use a solid color as a placeholder.
            table.BackgroundColor = Aspose.Pdf.Color.LightGray;

            // Add a row with three cells
            Row row = table.Rows.Add();
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");
            row.Cells.Add("Cell 3");

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the result
            doc.Save("output.pdf");
        }
    }
}