using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing; // needed for Graph and Rectangle shapes

class Program
{
    // Entry point: expects two arguments – input PDF path and output PDF path.
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: GraphAdder <input-pdf> <output-pdf>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document.
            using (Document doc = new Document(inputPath))
            {
                // Ensure there is at least one page to host the graph.
                Page page = doc.Pages[1];

                // Create a simple bar‑graph representation using a table.
                Table graphTable = new Table();
                // Two columns: Category label and visual bar.
                graphTable.ColumnWidths = "100 300";

                // Header row.
                Row headerRow = graphTable.Rows.Add();
                AddCell(headerRow, "Category", true);
                AddCell(headerRow, "Value", true);

                // Data rows – each bar's length is represented by a colored rectangle.
                AddDataRow(graphTable, "A", 70);
                AddDataRow(graphTable, "B", 40);
                AddDataRow(graphTable, "C", 90);

                // Add some spacing before the graph.
                page.Paragraphs.Add(new TextFragment("\n"));
                // Insert the table (graph) into the page.
                page.Paragraphs.Add(graphTable);

                // Save the modified PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Graph added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper to add a cell with optional header styling.
    private static void AddCell(Row row, string text, bool isHeader = false)
    {
        Cell cell = row.Cells.Add();
        TextFragment tf = new TextFragment(text);
        if (isHeader)
        {
            tf.TextState.FontSize = 12;
            tf.TextState.FontStyle = FontStyles.Bold;
            tf.TextState.ForegroundColor = Color.White;
            cell.BackgroundColor = Color.Gray;
        }
        else
        {
            tf.TextState.FontSize = 10;
            tf.TextState.ForegroundColor = Color.Black;
        }
        cell.Paragraphs.Add(tf);
    }

    // Helper to add a data row with a colored bar proportional to the value.
    private static void AddDataRow(Table table, string category, int value)
    {
        // Clamp value to a maximum of 100 for visual scaling.
        int clamped = Math.Max(0, Math.Min(100, value));

        Row dataRow = table.Rows.Add();

        // Category label cell.
        AddCell(dataRow, category);

        // Bar cell – we will place a Graph containing a Rectangle shape.
        Cell barCell = dataRow.Cells.Add();

        // Width of the bar (each unit = 3 points) and fixed height.
        float barWidth = clamped * 3f;
        float barHeight = 12f;

        // Create a Graph container (Graph derives from BaseParagraph, so it can be added to cell paragraphs).
        Graph barGraph = new Graph(barWidth, barHeight);

        // Create the rectangle shape that represents the bar.
        Aspose.Pdf.Drawing.Rectangle barShape = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, barWidth, barHeight);
        // Style the rectangle via GraphInfo.
        barShape.GraphInfo.FillColor = Color.FromRgb(51, 153, 204); // a teal colour (0.2,0.6,0.8)
        barShape.GraphInfo.Color = Color.Black; // optional border colour
        barShape.GraphInfo.LineWidth = 0.5f;

        // Add the rectangle to the graph.
        barGraph.Shapes.Add(barShape);

        // Add the graph to the cell.
        barCell.Paragraphs.Add(barGraph);

        // Optionally add the numeric value as text next to the bar.
        TextFragment valueTf = new TextFragment($" {value}");
        valueTf.TextState.FontSize = 10;
        valueTf.TextState.ForegroundColor = Color.Black;
        barCell.Paragraphs.Add(valueTf);
    }
}
