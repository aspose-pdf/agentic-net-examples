using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "formatted_table.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a table with three columns
            Table table = new Table
            {
                // Optional: set column widths (in points)
                ColumnWidths = "100 150 200"
            };

            // ---------- Header row ----------
            Row header = table.Rows.Add();
            Cell headerCell1 = header.Cells.Add("Header 1");
            Cell headerCell2 = header.Cells.Add("Header 2");
            Cell headerCell3 = header.Cells.Add("Header 3");

            // Apply background color to header cells
            headerCell1.BackgroundColor = Color.LightGray;
            headerCell2.BackgroundColor = Color.LightGray;
            headerCell3.BackgroundColor = Color.LightGray;

            // Center align header text
            headerCell1.Alignment = HorizontalAlignment.Center;
            headerCell2.Alignment = HorizontalAlignment.Center;
            headerCell3.Alignment = HorizontalAlignment.Center;

            // Add borders to header cells using BorderInfo (the correct class for tables/cells)
            headerCell1.Border = new BorderInfo(BorderSide.All, 1, Color.Black);
            headerCell2.Border = new BorderInfo(BorderSide.All, 1, Color.Black);
            headerCell3.Border = new BorderInfo(BorderSide.All, 1, Color.Black);

            // ---------- Data row ----------
            Row dataRow = table.Rows.Add();
            Cell dataCell1 = dataRow.Cells.Add("Row 1, Col 1");
            Cell dataCell2 = dataRow.Cells.Add("Row 1, Col 2");
            Cell dataCell3 = dataRow.Cells.Add("Row 1, Col 3");

            // Set background color for data cells
            dataCell1.BackgroundColor = Color.White;
            dataCell2.BackgroundColor = Color.White;
            dataCell3.BackgroundColor = Color.White;

            // Define a text state (font, size, color) for data cells
            TextState cellTextState = new TextState
            {
                Font = FontRepository.FindFont("Arial"),
                FontSize = 12,
                ForegroundColor = Color.Blue
            };
            dataCell1.DefaultCellTextState = cellTextState;
            dataCell2.DefaultCellTextState = cellTextState;
            dataCell3.DefaultCellTextState = cellTextState;

            // Set padding (margin) for data cells
            MarginInfo cellMargin = new MarginInfo { Top = 5, Bottom = 5, Left = 5, Right = 5 };
            dataCell1.Margin = cellMargin;
            dataCell2.Margin = cellMargin;
            dataCell3.Margin = cellMargin;

            // Add the table to the page. It will be placed at the current cursor position.
            page.Paragraphs.Add(table);

            // Optionally, position the table using a rectangle (uncomment if needed)
            // Aspose.Pdf.Rectangle tableRect = new Aspose.Pdf.Rectangle(50, 700, 550, 500);
            // page.Paragraphs.Add(new TableFragment(table, tableRect));

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with formatted table saved to '{outputPath}'.");
    }
}
