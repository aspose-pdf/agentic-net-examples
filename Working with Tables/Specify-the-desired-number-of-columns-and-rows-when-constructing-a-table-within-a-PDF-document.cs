using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table.pdf";

        // Desired number of rows and columns – defined outside the using block so they are in scope for the whole method
        int desiredRows = 4;
        int desiredColumns = 3;

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a table instance
            Table table = new Table();

            // Define three equal column widths (optional, but clarifies column count)
            table.ColumnWidths = "150 150 150";

            // Populate the table with the specified rows and columns
            for (int r = 0; r < desiredRows; r++)
            {
                // Add a new row to the table
                Row row = table.Rows.Add();

                for (int c = 0; c < desiredColumns; c++)
                {
                    // Add a new cell to the current row
                    Cell cell = row.Cells.Add();

                    // Insert a simple text fragment into the cell for demonstration
                    cell.Paragraphs.Add(new TextFragment($"R{r + 1}C{c + 1}"));
                }
            }

            // Add the fully constructed table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the document to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with a {desiredRows}x{desiredColumns} table saved to '{outputPath}'.");
    }
}
