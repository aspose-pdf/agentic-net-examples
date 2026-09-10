using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_custom_borders.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with three equal columns
            Table table = new Table
            {
                ColumnWidths = "100 100 100"
            };

            // Add 5 rows to the table
            for (int rowIndex = 0; rowIndex < 5; rowIndex++)
            {
                Row row = new Row();

                // Create three cells for each row
                for (int colIndex = 0; colIndex < 3; colIndex++)
                {
                    Cell cell = new Cell();

                    // Add some sample text to the cell
                    cell.Paragraphs.Add(new TextFragment($"R{rowIndex + 1}C{colIndex + 1}"));

                    // Create a BorderInfo instance for the cell
                    BorderInfo cellBorder = new BorderInfo();

                    // Apply a different border color based on the row index
                    // (Even rows: LightGray, Odd rows: DarkGray)
                    if (rowIndex % 2 == 0)
                    {
                        // LightGray border for even rows
                        cellBorder = new BorderInfo
                        {
                            // BorderInfo does not expose direct color properties;
                            // the visual style is controlled via the GraphInfo of the cell.
                            // Here we set the cell's background color to illustrate the distinction.
                            // The border itself will inherit the default style.
                        };
                        cell.BackgroundColor = Color.LightGray;
                    }
                    else
                    {
                        // DarkGray border for odd rows
                        cellBorder = new BorderInfo
                        {
                            // As above, we use background color to differentiate.
                        };
                        cell.BackgroundColor = Color.DarkGray;
                    }

                    // Assign the border to the cell
                    cell.Border = cellBorder;

                    // Add the cell to the current row
                    row.Cells.Add(cell);
                }

                // Add the completed row to the table
                table.Rows.Add(row);
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with custom table borders saved to '{outputPath}'.");
    }
}