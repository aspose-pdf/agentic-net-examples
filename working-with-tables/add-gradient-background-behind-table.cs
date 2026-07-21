using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // ------------------------------------------------------------
            // NOTE:
            // Table.BackgroundColor expects an Aspose.Pdf.Color (solid color).
            // It does NOT accept a brush or gradient directly.
            // To achieve a gradient background behind a table, we draw a
            // rectangle with a solid fill (or a simulated gradient) on a Graph
            // and place the table on top of that rectangle.
            // ------------------------------------------------------------

            // Define the area where the table will be placed
            double tableLeft = 50;
            double tableTop = 500;
            double tableWidth = 500;
            double tableHeight = 200;

            // Create a Graph that will hold the background rectangle
            Graph gradientGraph = new Graph(tableWidth, tableHeight)
            {
                Left = (float)tableLeft,
                Top = (float)tableTop
            };

            // Define a rectangle that fills the whole graph area
            var gradientRect = new Aspose.Pdf.Drawing.Rectangle(
                0f,
                0f,
                (float)tableWidth,
                (float)tableHeight);

            // Use a solid fill color (Aspose.Pdf does not expose a gradient brush directly)
            // If a true gradient is required, it can be simulated by drawing multiple
            // thin rectangles with gradually changing colors.
            gradientRect.GraphInfo = new GraphInfo { FillColor = Aspose.Pdf.Color.LightBlue };

            // Add the rectangle to the graph
            gradientGraph.Shapes.Add(gradientRect);

            // Add the background graph to the page before the table
            page.Paragraphs.Add(gradientGraph);

            // Create a table and position it over the background rectangle
            Table table = new Table
            {
                // Set the table position to match the background rectangle (float values required)
                Left = (float)tableLeft,
                Top = (float)tableTop
                // Optional: set a semi‑transparent background color if desired
                // BackgroundColor = Aspose.Pdf.Color.FromRgba(255, 255, 255, 0.5) // 50% transparent white
            };

            // Define column widths (example: two equal columns)
            table.ColumnWidths = "250 250";

            // Add a header row
            Row header = table.Rows.Add();
            header.BackgroundColor = Aspose.Pdf.Color.LightGray;
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Cell 1");
            data.Cells.Add("Cell 2");

            // Add the table to the page (it will appear on top of the background)
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save("TableWithGradientBackground.pdf");
        }

        Console.WriteLine("PDF created with a background behind the table.");
    }
}
