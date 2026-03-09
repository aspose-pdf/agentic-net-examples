using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "FormattedTable.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a table and configure its visual appearance
            Table table = new Table();

            // Table alignment on the page
            table.Alignment = HorizontalAlignment.Center;

            // Table border (outer border)
            table.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.DarkGray);

            // Default cell border
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray);

            // Default cell padding
            table.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);

            // Default text style for all cells
            table.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.Black
            };

            // Define column widths (space‑separated values, in points)
            table.ColumnWidths = "100 150 200";

            // Enable pagination: break the table vertically across pages
            table.Broken = TableBroken.VerticalInSamePage;

            // Repeat the first row (header) on each new page
            table.RepeatingRowsCount = 1;

            // ---------- Header Row ----------
            Row header = table.Rows.Add();
            // Header background
            header.BackgroundColor = Aspose.Pdf.Color.LightGray;
            // Header cells
            Cell cell1 = header.Cells.Add("Product");
            Cell cell2 = header.Cells.Add("Description");
            Cell cell3 = header.Cells.Add("Price");

            // Header text style (bold)
            TextState headerTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Bold"),
                FontSize = 13,
                ForegroundColor = Aspose.Pdf.Color.Black
            };
            cell1.DefaultCellTextState = headerTextState;
            cell2.DefaultCellTextState = headerTextState;
            cell3.DefaultCellTextState = headerTextState;

            // ---------- Data Rows ----------
            // Add enough rows to force a page break
            for (int i = 1; i <= 50; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Item {i}");
                row.Cells.Add($"This is a description for item {i}.");
                row.Cells.Add($"${i * 10:0.00}");

                // Example: alternate row background color
                if (i % 2 == 0)
                {
                    row.BackgroundColor = Aspose.Pdf.Color.FromRgb(0.95, 0.95, 0.95);
                }
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with formatted table saved to '{outputPath}'.");
    }
}