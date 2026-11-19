using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_example.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Table object
            Table table = new Table
            {
                // Optional: set a border for the whole table
                Border = new BorderInfo(BorderSide.All, 0.5f),
                // Optional: define column widths (three equal columns)
                ColumnWidths = "100 100 100"
            };

            // ----- First row (header) -----
            Row headerRow = table.Rows.Add();

            // Add cells to the header row
            Cell headerCell1 = headerRow.Cells.Add("Header 1");
            Cell headerCell2 = headerRow.Cells.Add("Header 2");
            Cell headerCell3 = headerRow.Cells.Add("Header 3");

            // Apply formatting to header cells
            foreach (Cell cell in headerRow.Cells)
            {
                cell.BackgroundColor = Aspose.Pdf.Color.LightGray; // cross‑platform color
                cell.DefaultCellTextState = new TextState
                {
                    FontSize = 12,
                    FontStyle = FontStyles.Bold,
                    Font = FontRepository.FindFont("Arial")
                };
                cell.Alignment = HorizontalAlignment.Center;
            }

            // ----- Second row -----
            Row row2 = table.Rows.Add();
            row2.Cells.Add("Row 1, Col 1");
            row2.Cells.Add("Row 1, Col 2");
            row2.Cells.Add("Row 1, Col 3");

            // ----- Third row -----
            Row row3 = table.Rows.Add();
            row3.Cells.Add("Row 2, Col 1");
            row3.Cells.Add("Row 2, Col 2");
            row3.Cells.Add("Row 2, Col 3");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }
}