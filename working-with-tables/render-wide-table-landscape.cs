using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextFragment if needed

class Program
{
    static void Main()
    {
        const string outputPath = "wide_table_landscape.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page and set its orientation to Landscape by swapping width/height
            Page page = doc.Pages.Add();
            // Set page size to A4 dimensions but in landscape orientation
            page.PageInfo.Width = PageSize.A4.Height;   // 842 points
            page.PageInfo.Height = PageSize.A4.Width;  // 595 points

            // Create a visual Table that will be wide enough to require landscape orientation
            Table table = new Table
            {
                // Define column widths (example: five equal columns)
                ColumnWidths = "100 100 100 100 100",
                // Optional: set border for visibility
                Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.BackgroundColor = Aspose.Pdf.Color.LightGray;
            for (int c = 1; c <= 5; c++)
            {
                Cell cell = header.Cells.Add();
                TextFragment tf = new TextFragment($"Header {c}");
                tf.TextState.FontSize = 12;
                tf.TextState.FontStyle = FontStyles.Bold;
                cell.Paragraphs.Add(tf);
            }

            // Add several data rows to demonstrate width
            for (int r = 1; r <= 10; r++)
            {
                Row row = table.Rows.Add();
                for (int c = 1; c <= 5; c++)
                {
                    Cell cell = row.Cells.Add();
                    TextFragment tf = new TextFragment($"R{r}C{c}");
                    tf.TextState.FontSize = 10;
                    cell.Paragraphs.Add(tf);
                }
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with landscape orientation saved to '{outputPath}'.");
    }
}
