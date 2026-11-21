using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "styled_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to host the table.
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create a table with three columns (adjust widths as needed).
            Table table = new Table
            {
                ColumnWidths = "150 150 150", // three equal columns
                Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.LightGray) // optional outer border
            };

            // Helper to style a cell uniformly.
            void StyleCell(Cell cell)
            {
                // Background color for visual contrast.
                cell.BackgroundColor = Aspose.Pdf.Color.FromRgb(0.9, 0.9, 0.95);

                // Border thickness and color.
                cell.Border = new BorderInfo(BorderSide.All, 1.0f, Aspose.Pdf.Color.Black);

                // Padding (implemented via MarginInfo on the cell).
                cell.Margin = new MarginInfo(5, 5, 5, 5);
            }

            // Add header row.
            Row headerRow = table.Rows.Add();
            string[] headers = { "Product", "Quantity", "Price" };
            foreach (string hdr in headers)
            {
                Cell hdrCell = headerRow.Cells.Add(hdr);
                StyleCell(hdrCell);
                // Make header text bold.
                hdrCell.DefaultCellTextState = new TextState
                {
                    FontSize = 12,
                    FontStyle = FontStyles.Bold,
                    ForegroundColor = Aspose.Pdf.Color.Black
                };
            }

            // Add sample data rows.
            string[,] data = {
                { "Widget A", "10", "$15.00" },
                { "Widget B", "5",  "$22.50" },
                { "Widget C", "12", "$9.75" }
            };

            for (int r = 0; r < data.GetLength(0); r++)
            {
                Row dataRow = table.Rows.Add();
                for (int c = 0; c < data.GetLength(1); c++)
                {
                    Cell dataCell = dataRow.Cells.Add(data[r, c]);
                    StyleCell(dataCell);
                    dataCell.DefaultCellTextState = new TextState
                    {
                        FontSize = 11,
                        FontStyle = FontStyles.Regular,
                        ForegroundColor = Aspose.Pdf.Color.Black
                    };
                }
            }

            // Add the styled table to the page.
            page.Paragraphs.Add(table);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with styled table saved to '{outputPath}'.");
    }
}
