using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // Graph, Rectangle, Color, etc.

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_shadow_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Add a new page where the table will be placed
            Page page = doc.Pages.Add();

            // Create a table with two columns
            Table table = new Table
            {
                ColumnWidths = "200 200", // set column widths
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");

            // Add a couple of data rows
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Cell 1");
            row1.Cells.Add("Cell 2");

            Row row2 = table.Rows.Add();
            row2.Cells.Add("Cell 3");
            row2.Cells.Add("Cell 4");

            // ------------------------------------------------------------
            // Simulate a shadow for the table.
            // ------------------------------------------------------------
            // Estimate table dimensions: total column width + some padding.
            float tableWidth = 200 + 200; // matches ColumnWidths
            // Approximate row height (default) – 20 points per row works well.
            float rowHeight = 20f;
            // Include header row in the height calculation.
            int totalRows = 1 + table.Rows.Count; // 1 header + data rows
            float tableHeight = totalRows * rowHeight;

            // Create a Graph container that will hold the shadow rectangle.
            // Use the double‑based constructor (the float overload is obsolete).
            Graph shadowGraph = new Graph((double)tableWidth + 10.0, (double)tableHeight + 10.0);

            // Rectangle positioned with an offset (e.g., 5 points right, -5 points up).
            Aspose.Pdf.Drawing.Rectangle shadowRect = new Aspose.Pdf.Drawing.Rectangle(5, -5, tableWidth, tableHeight);
            // Configure the rectangle's appearance via GraphInfo.
            shadowRect.GraphInfo.FillColor = Color.LightGray; // fill colour for the shadow
            // No border is required; leaving the stroke colour transparent.
            shadowRect.GraphInfo.Color = Color.Transparent;
            shadowRect.GraphInfo.LineWidth = 0;

            shadowGraph.Shapes.Add(shadowRect);

            // Add the shadow graph first so it appears behind the table.
            page.Paragraphs.Add(shadowGraph);
            // Then add the actual table.
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with shadowed table saved to '{outputPath}'.");
    }
}
