using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "TableWithValues.pdf";

        // Create a new PDF document inside a using block (ensures disposal)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position on the page
            Table table = new Table
            {
                // Position the table (left, bottom, right, top)
                // Note: Table does not have a direct Rectangle property; we set Left and Top
                Left = 50,
                Top = 500
            };

            // Define column widths (Aspose.Pdf.Table.ColumnWidths is a *string* property,
            // not a collection, so we assign a space‑separated list of widths.)
            table.ColumnWidths = "150 150 150";

            // Add three rows with three cells each and assign values
            for (int rowIndex = 0; rowIndex < 3; rowIndex++)
            {
                // Add a new row to the table
                Row row = table.Rows.Add();

                for (int colIndex = 0; colIndex < 3; colIndex++)
                {
                    // Add a new cell to the current row
                    Cell cell = row.Cells.Add();

                    // Create the text that will be placed in the cell
                    string cellText = $"R{rowIndex + 1}C{colIndex + 1}";

                    // Add a TextFragment to the cell's Paragraphs collection
                    cell.Paragraphs.Add(new TextFragment(cellText));

                    // Optional: set cell background color for visual distinction
                    if ((rowIndex + colIndex) % 2 == 0)
                    {
                        cell.BackgroundColor = Aspose.Pdf.Color.LightGray;
                    }
                }
            }

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }
}
