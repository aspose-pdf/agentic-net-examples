using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

namespace TableBorderExample
{
    class Program
    {
        static void Main()
        {
            const string outputPath = "TableWithRowBorders.pdf";

            // Create a new PDF document
            using (Document doc = new Document())
            {
                // Add a page to the document
                Page page = doc.Pages.Add();

                // Create a table and add it to the page
                Table table = new Table
                {
                    // Define three equal column widths
                    ColumnWidths = "150 150 150",
                    // Optional: set a light gray background for the whole table
                    BackgroundColor = Aspose.Pdf.Color.FromRgb(0.95, 0.95, 0.95)
                };
                page.Paragraphs.Add(table);

                // Sample data for the table (header + 5 data rows)
                string[,] data = new string[,] {
                    { "ID", "Name", "Score" },
                    { "1", "Alice", "85" },
                    { "2", "Bob", "92" },
                    { "3", "Charlie", "78" },
                    { "4", "Diana", "88" },
                    { "5", "Ethan", "91" }
                };

                // Iterate over the data and create rows
                for (int i = 0; i < data.GetLength(0); i++)
                {
                    // Create a new row and add it to the table
                    Row row = new Row();
                    table.Rows.Add(row);

                    // Apply custom border style based on row index (i)
                    // Header row (i == 0) -> thick black border
                    // Even data rows (i % 2 == 0) -> red thick border
                    // Odd data rows -> blue thin border
                    if (i == 0)
                    {
                        // Header row styling
                        row.DefaultCellBorder = new BorderInfo(BorderSide.All, 1.5f);
                        row.DefaultCellBorder.Color = Aspose.Pdf.Color.Black;
                        row.BackgroundColor = Aspose.Pdf.Color.LightGray;
                    }
                    else if (i % 2 == 0)
                    {
                        // Even row styling
                        row.DefaultCellBorder = new BorderInfo(BorderSide.All, 1.0f);
                        row.DefaultCellBorder.Color = Aspose.Pdf.Color.Red;
                        row.BackgroundColor = Aspose.Pdf.Color.FromRgb(0.98, 0.9, 0.9);
                    }
                    else
                    {
                        // Odd row styling
                        row.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f);
                        row.DefaultCellBorder.Color = Aspose.Pdf.Color.Blue;
                        row.BackgroundColor = Aspose.Pdf.Color.FromRgb(0.9, 0.9, 0.98);
                    }

                    // Add cells to the current row
                    for (int j = 0; j < data.GetLength(1); j++)
                    {
                        Cell cell = new Cell
                        {
                            // Set the text for the cell
                            Paragraphs = { new TextFragment(data[i, j]) },
                            // Ensure the cell uses the row's default border settings
                            Border = row.DefaultCellBorder,
                            // Optional: center the text vertically and horizontally
                            VerticalAlignment = VerticalAlignment.Center,
                            Alignment = HorizontalAlignment.Center
                        };
                        row.Cells.Add(cell);
                    }
                }

                // Save the PDF document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
    }
}
