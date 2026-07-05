using System;
using System.IO;
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

            // Create a table that will span multiple pages
            Table table = new Table
            {
                // Position the table on the page
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent,
                // Repeat the first row (header) on each new page
                RepeatingRowsCount = 1,
                // Repeat the first column on each new page when the table splits horizontally
                RepeatingColumnsCount = 1,
                // Define column widths (example: three columns)
                ColumnWidths = "100 150 200"
            };

            // ----- Header row (will be repeated) -----
            Row header = table.Rows.Add();
            header.Cells.Add("ID");
            header.Cells.Add("Name");
            header.Cells.Add("Description");
            // Apply a bold style to the header row
            header.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Bold"),
                FontSize = 12,
                ForegroundColor = Color.Black
            };

            // ----- Data rows (enough to cause a page break) -----
            for (int i = 1; i <= 50; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add(i.ToString());                     // First column (will be repeated)
                row.Cells.Add($"Item {i}");                      // Second column
                row.Cells.Add($"This is a description for item {i}. It may be long enough to wrap onto multiple lines."); // Third column
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save("Table_With_Repeating_Header_And_Column.pdf");
        }

        Console.WriteLine("PDF created with repeating header row and first column.");
    }
}
