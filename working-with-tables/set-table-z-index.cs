using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "table_zindex.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Add a background rectangle (optional visual element)
            // Graph constructor expects double values
            Graph graph = new Graph(500.0, 800.0);
            // Use Aspose.Pdf.Drawing.Rectangle for shapes inside a Graph
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50f, 600f, 400f, 200f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rect);
            page.Paragraphs.Add(graph);

            // Create a table and set its ZIndex to a higher value so it appears above other elements
            Table table = new Table
            {
                Left = 100,          // X position
                Top = 650,           // Y position
                ZIndex = 10,         // Higher Z-order => drawn on top
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Black),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Define three equal-width columns
            table.ColumnWidths = "100 100 100";

            // Add a header row with background color
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");
            foreach (Cell cell in header.Cells)
            {
                cell.BackgroundColor = Color.LightBlue;
            }

            // Add a few data rows
            for (int i = 0; i < 3; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"R{i + 1}C1");
                row.Cells.Add($"R{i + 1}C2");
                row.Cells.Add($"R{i + 1}C3");
            }

            // Place the table on the page
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
