using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades; // included as requested

class Program
{
    static void Main()
    {
        const string outputPath = "styled_table.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with three columns
            Table table = new Table
            {
                // Define column widths (points)
                ColumnWidths = "100 100 100",
                // Default border for all cells (thin line)
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                // Default padding for all cells
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // ----- Header row -----
            Row headerRow = table.Rows.Add();
            headerRow.BackgroundColor = Aspose.Pdf.Color.LightGray; // distinct header background

            AddCell(headerRow, "Header 1");
            AddCell(headerRow, "Header 2");
            AddCell(headerRow, "Header 3");

            // ----- Data rows -----
            for (int i = 1; i <= 3; i++)
            {
                Row dataRow = table.Rows.Add();
                // Light background for data rows (optional)
                dataRow.BackgroundColor = Aspose.Pdf.Color.FromRgb(0.95f, 0.95f, 0.95f);

                AddCell(dataRow, $"Row {i} Col 1");
                AddCell(dataRow, $"Row {i} Col 2");
                AddCell(dataRow, $"Row {i} Col 3");
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with styled table saved to '{outputPath}'.");
    }

    // Helper method to create a styled cell and add it to a row
    static void AddCell(Row row, string text)
    {
        Cell cell = new Cell
        {
            // White background for individual cells
            BackgroundColor = Aspose.Pdf.Color.White,
            // Border thickness of 1 point on all sides
            Border = new BorderInfo(BorderSide.All, 1f),
            // Padding inside the cell
            Margin = new MarginInfo(2, 2, 2, 2)
        };

        // Add text to the cell
        cell.Paragraphs.Add(new TextFragment(text));

        // Append the cell to the row
        row.Cells.Add(cell);
    }
}