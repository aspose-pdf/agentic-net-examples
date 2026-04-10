using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_output.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (first page is created automatically, but we add one explicitly for clarity)
            Page page = doc.Pages.Add();

            // Create a table and configure its appearance
            Table table = new Table();

            // Set overall table border using the appropriate BorderInfo constructor
            table.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black);

            // Set default cell border using the appropriate BorderInfo constructor
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray);
            table.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);

            // Define column widths (5 columns, each 100 points wide)
            // ColumnWidths is a string property, not a collection
            table.ColumnWidths = "100 100 100 100 100";

            // Add rows with alternating background colors
            for (int r = 0; r < 10; r++)
            {
                Row row = table.Rows.Add();

                // Alternate row background: LightGray for even rows, White for odd rows
                row.BackgroundColor = (r % 2 == 0) ? Aspose.Pdf.Color.LightGray : Aspose.Pdf.Color.White;

                // Populate cells in the current row
                for (int c = 0; c < 5; c++)
                {
                    // Add a cell with simple text indicating its position
                    Cell cell = new Cell();
                    cell.Paragraphs.Add(new TextFragment($"R{r + 1}C{c + 1}"));
                    row.Cells.Add(cell);
                }
            }

            // Optional: set table margin within the page
            table.Margin = new MarginInfo(20, 20, 20, 20);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with styled table saved to '{outputPath}'.");
    }
}
