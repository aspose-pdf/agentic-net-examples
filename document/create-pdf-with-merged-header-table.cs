using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "TableWithMergedHeader.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a table with three columns of equal width
            Table table = new Table
            {
                ColumnWidths = "100 100 100"
            };

            // ---------- First header row (merged cell spanning all columns) ----------
            Row mergedHeaderRow = table.Rows.Add();
            Cell mergedHeaderCell = mergedHeaderRow.Cells.Add("Merged Header");
            mergedHeaderCell.ColSpan = 3; // Span across three columns
            mergedHeaderCell.BackgroundColor = Aspose.Pdf.Color.LightGray;
            mergedHeaderCell.Margin = new MarginInfo { Top = 5, Bottom = 5 };
            // Use DefaultCellTextState for alignment and font settings
            mergedHeaderCell.DefaultCellTextState = new TextState
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.Black
            };

            // ---------- Second header row (individual column titles) ----------
            Row columnHeaderRow = table.Rows.Add();
            columnHeaderRow.Cells.Add("Column 1");
            columnHeaderRow.Cells.Add("Column 2");
            columnHeaderRow.Cells.Add("Column 3");

            // Apply styling to the second header row
            foreach (Cell cell in columnHeaderRow.Cells)
            {
                cell.BackgroundColor = Aspose.Pdf.Color.FromRgb(0.8, 0.8, 0.8);
                cell.Margin = new MarginInfo { Top = 3, Bottom = 3 };
                cell.DefaultCellTextState = new TextState
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12,
                    ForegroundColor = Aspose.Pdf.Color.Black
                };
            }

            // ---------- Data rows ----------
            for (int i = 1; i <= 5; i++)
            {
                Row dataRow = table.Rows.Add();
                dataRow.Cells.Add($"Row {i} - A");
                dataRow.Cells.Add($"Row {i} - B");
                dataRow.Cells.Add($"Row {i} - C");
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with merged header table saved to '{outputPath}'.");
    }
}
