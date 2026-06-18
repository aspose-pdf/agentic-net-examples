using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF with a single blank page
        using (Document createDoc = new Document())
        {
            createDoc.Pages.Add();
            createDoc.Save("input.pdf");
        }

        // Open the sample PDF and add a rotated table
        using (Document doc = new Document("input.pdf"))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Rotate the page 90 degrees clockwise (equivalent to applying a 90° transformation matrix)
            page.Rotate = Rotation.on90;

            // Create a simple 2x2 table
            Table table = new Table();
            table.ColumnWidths = "100 100";

            // Set a simple border for the table cells
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f);

            // First row (header)
            Row headerRow = table.Rows.Add();
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");

            // Second row (data)
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Cell 1");
            dataRow.Cells.Add("Cell 2");

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the result
            doc.Save("output.pdf");
        }
    }
}