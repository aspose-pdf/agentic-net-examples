using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path for the output PDF
        const string outputPath = "TableColumnRemoved.pdf";

        // Specify which column to remove (0‑based index)
        int columnToRemove = 1; // removes the second column

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with 4 columns and 3 rows
            Table table = new Table();

            for (int i = 0; i < 3; i++)
            {
                // Add a new row
                Row row = table.Rows.Add();

                // Populate the row with 4 cells
                for (int j = 0; j < 4; j++)
                {
                    // Each cell contains simple text indicating its position
                    row.Cells.Add($"R{i + 1}C{j + 1}");
                }
            }

            // Delete the cell at the specified column index from every row
            foreach (Row row in table.Rows)
            {
                if (columnToRemove < row.Cells.Count)
                {
                    // Cells collection does not expose RemoveAt; remove by value instead
                    var cellToRemove = row.Cells[columnToRemove];
                    row.Cells.Remove(cellToRemove);
                }
            }

            // Add the modified table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'. Column {columnToRemove + 1} has been removed.");
    }
}
