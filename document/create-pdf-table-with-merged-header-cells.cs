using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "merged_header_table.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position on the page
            Table table = new Table
            {
                // Position the table (optional)
                Left = 50,
                Top = 700,
                // Define three columns with specific widths
                ColumnWidths = "100 150 150"
            };

            // ---------- Header row with merged cells ----------
            // Add the first header row
            Row headerRow = table.Rows.Add();

            // First cell spans two columns
            Cell mergedHeader = headerRow.Cells.Add("Merged Header");
            mergedHeader.ColSpan = 2; // Span across the first two columns
            mergedHeader.DefaultCellTextState = new TextState
            {
                FontSize = 14,
                FontStyle = FontStyles.Bold,
                ForegroundColor = Color.Black,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            // Third column header (standalone)
            Cell thirdHeader = headerRow.Cells.Add("Third Column");
            thirdHeader.DefaultCellTextState = new TextState
            {
                FontSize = 14,
                FontStyle = FontStyles.Bold,
                ForegroundColor = Color.Black,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            // ---------- Second header row (sub‑headers) ----------
            Row subHeaderRow = table.Rows.Add();

            // Sub‑header for first logical column
            Cell subHeader1 = subHeaderRow.Cells.Add("Sub‑Header 1");
            subHeader1.DefaultCellTextState = new TextState
            {
                FontSize = 12,
                FontStyle = FontStyles.Bold,
                ForegroundColor = Color.DarkGray,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            // Sub‑header for second logical column
            Cell subHeader2 = subHeaderRow.Cells.Add("Sub‑Header 2");
            subHeader2.DefaultCellTextState = new TextState
            {
                FontSize = 12,
                FontStyle = FontStyles.Bold,
                ForegroundColor = Color.DarkGray,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            // Sub‑header for third column (align with previous third header)
            Cell subHeader3 = subHeaderRow.Cells.Add("Sub‑Header 3");
            subHeader3.DefaultCellTextState = new TextState
            {
                FontSize = 12,
                FontStyle = FontStyles.Bold,
                ForegroundColor = Color.DarkGray,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            // ---------- Data rows ----------
            for (int i = 1; i <= 5; i++)
            {
                Row dataRow = table.Rows.Add();

                Cell cell1 = dataRow.Cells.Add($"Row {i} - Col 1");
                Cell cell2 = dataRow.Cells.Add($"Row {i} - Col 2");
                Cell cell3 = dataRow.Cells.Add($"Row {i} - Col 3");

                // Optional: set text alignment for data cells
                cell1.DefaultCellTextState = new TextState { HorizontalAlignment = HorizontalAlignment.Center };
                cell2.DefaultCellTextState = new TextState { HorizontalAlignment = HorizontalAlignment.Center };
                cell3.DefaultCellTextState = new TextState { HorizontalAlignment = HorizontalAlignment.Center };
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with merged header cells saved to '{outputPath}'.");
    }
}