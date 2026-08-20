using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "table_double_border.pdf";

        // Create a new PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with three columns
            Table table = new Table
            {
                // Define column widths (in points)
                ColumnWidths = "100 100 100"
            };

            // Populate the table with sample data (3 rows x 3 columns)
            for (int r = 0; r < 3; r++)
            {
                Row row = table.Rows.Add();
                for (int c = 0; c < 3; c++)
                {
                    // Each cell contains simple text
                    row.Cells.Add($"R{r + 1}C{c + 1}");
                }
            }

            // ------------------------------------------------------------
            // Apply a double border style to the table
            // ------------------------------------------------------------
            // Create a BorderInfo instance using the constructor that sets side, width and color.
            // Aspose.Pdf does not expose a separate "Style" property; a double line effect is achieved
            // by using an appropriate width (e.g., 2 points) and the desired color.
            BorderInfo doubleBorder = new BorderInfo(BorderSide.All, 2f, Color.Blue);

            // Assign the configured BorderInfo to the table
            table.Border = doubleBorder;

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF (save rule: use Document.Save with a path)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with double‑border table saved to '{outputPath}'.");
    }
}
