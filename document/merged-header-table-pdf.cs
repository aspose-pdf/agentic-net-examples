using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

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

            // -------------------------------------------------
            // Header table (single cell that visually spans columns)
            // -------------------------------------------------
            // The width of this table equals the total width of the three columns of the data table (150+150+150 = 450 points)
            Table headerTable = new Table
            {
                ColumnWidths = "450",
                Border = new BorderInfo(BorderSide.All, 1, Color.Black),
                // Optional: give the header a little margin from the data table
                Margin = new MarginInfo { Bottom = 5 }
            };

            Row headerRow = headerTable.Rows.Add();
            Cell headerCell = headerRow.Cells.Add();
            headerCell.BackgroundColor = Color.LightGray;

            // Add centered text to the header cell
            TextFragment headerText = new TextFragment("Merged Header Across All Columns")
            {
                TextState = { FontSize = 14, Font = FontRepository.FindFont("Helvetica"), ForegroundColor = Color.Blue }
            };
            // Center the paragraph inside the cell
            headerCell.Paragraphs.Add(headerText);
            headerCell.Paragraphs[0].HorizontalAlignment = HorizontalAlignment.Center;

            // Add the header table to the page
            page.Paragraphs.Add(headerTable);

            // -------------------------------------------------
            // Data table with three equal‑width columns
            // -------------------------------------------------
            Table dataTable = new Table
            {
                ColumnWidths = "150 150 150",
                Border = new BorderInfo(BorderSide.All, 1, Color.Black)
            };

            // Add data rows
            for (int i = 1; i <= 4; i++)
            {
                Row dataRow = dataTable.Rows.Add();

                // First column
                Cell cell1 = dataRow.Cells.Add();
                cell1.Paragraphs.Add(new TextFragment($"Row {i} - Col 1"));

                // Second column
                Cell cell2 = dataRow.Cells.Add();
                cell2.Paragraphs.Add(new TextFragment($"Row {i} - Col 2"));

                // Third column
                Cell cell3 = dataRow.Cells.Add();
                cell3.Paragraphs.Add(new TextFragment($"Row {i} - Col 3"));
            }

            // Add the data table to the page
            page.Paragraphs.Add(dataTable);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with merged header table saved to '{outputPath}'.");
    }
}
