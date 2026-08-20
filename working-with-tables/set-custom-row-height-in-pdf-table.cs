using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with three columns
            Table table = new Table();
            table.ColumnWidths = "150 150 150";

            // First row (default height)
            Row row1 = table.Rows.Add();
            row1.Cells.Add("R1C1");
            row1.Cells.Add("R1C2");
            row1.Cells.Add("R1C3");

            // Second row – set a custom fixed height (e.g., 80 points)
            Row row2 = table.Rows.Add();
            row2.Cells.Add("R2C1");
            row2.Cells.Add("R2C2");
            row2.Cells.Add("R2C3");
            row2.FixedRowHeight = 80; // Assign fixed height in points

            // Third row (default height)
            Row row3 = table.Rows.Add();
            row3.Cells.Add("R3C1");
            row3.Cells.Add("R3C2");
            row3.Cells.Add("R3C3");

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF to a file
            doc.Save("TableWithCustomRowHeight.pdf");
        }
    }
}