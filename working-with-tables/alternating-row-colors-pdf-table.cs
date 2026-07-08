using System;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Added for TextState and FontStyles

class Program
{
    static void Main()
    {
        const string outputPath = "alternating_table.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with three columns
            Table table = new Table
            {
                ColumnWidths = "100 100 100",
                Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
            };

            // Header row
            Row header = table.Rows.Add();
            header.DefaultCellTextState = new TextState { FontSize = 12, FontStyle = FontStyles.Bold };
            header.Cells.Add("ID");
            header.Cells.Add("Name");
            header.Cells.Add("Value");

            // Sample data rows
            for (int i = 1; i <= 10; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add(i.ToString());
                row.Cells.Add($"Item {i}");
                row.Cells.Add((i * 10).ToString());
            }

            // Apply alternating background colors to cells based on row index parity
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Row row = table.Rows[i];
                Aspose.Pdf.Color bg = (i % 2 == 0) ? Aspose.Pdf.Color.LightGray : Aspose.Pdf.Color.White;
                foreach (Cell cell in row.Cells)
                {
                    cell.BackgroundColor = bg;
                }
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
