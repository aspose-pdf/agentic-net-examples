using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "TableWithRepeatingHeader.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position on the page
            Table table = new Table
            {
                // Position the table (left, top)
                Left = 50,
                Top = 750,
                // Enable breaking across pages
                IsBroken = true,
                // Number of header rows to repeat on each page
                RepeatingRowsCount = 1,
                // Optional: style for the repeating header rows
                RepeatingRowsStyle = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica-Bold"),
                    FontSize = 12,
                    ForegroundColor = Color.Black
                }
            };

            // Define column widths (percentage of the page width)
            table.ColumnWidths = "30 70";

            // ----- Header Row (will be repeated) -----
            Row header = table.Rows.Add();
            // First cell (header)
            Cell headerCell1 = header.Cells.Add("ID");
            // Second cell (header)
            Cell headerCell2 = header.Cells.Add("Description");

            // Apply header styling
            headerCell1.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Bold"),
                FontSize = 12,
                ForegroundColor = Color.White
            };
            headerCell2.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Bold"),
                FontSize = 12,
                ForegroundColor = Color.White
            };
            // Background color for header row
            header.BackgroundColor = Color.Gray;

            // ----- Data Rows (enough to span multiple pages) -----
            for (int i = 1; i <= 200; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Item {i}");
                row.Cells.Add($"This is a description for item number {i}.");
            }

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'. Header rows will repeat on each page.");
    }
}