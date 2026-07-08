using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string outputPath = "table_with_background.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // ------------------------------------------------------------
            // NOTE:
            // Aspose.Pdf.Table.BackgroundColor property accepts a solid
            // Aspose.Pdf.Color value only. There is no API that allows a
            // LinearGradientBrush (or any gradient brush) to be assigned
            // directly to this property. To achieve a gradient effect you
            // would need to draw a rectangle with a gradient fill behind
            // the table using the Drawing.Graph API. The code below shows
            // how to add a solid background color to the table (which
            // compiles) and includes commented guidance for a gradient
            // approach.
            // ------------------------------------------------------------

            // Create a table and set a solid background color
            Table table = new Table
            {
                // Solid background color (example: light blue)
                BackgroundColor = Color.FromRgb(0.8, 0.9, 1.0),

                // Optional: set column widths and other properties
                ColumnWidths = "100 150 100"
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.BackgroundColor = Color.LightGray;
            header.Cells.Add("ID");
            header.Cells.Add("Name");
            header.Cells.Add("Quantity");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("1");
            data.Cells.Add("Apples");
            data.Cells.Add("50");

            // Position the table on the page
            table.Margin = new MarginInfo { Top = 100, Left = 50 };
            page.Paragraphs.Add(table);

            // ------------------------------------------------------------
            // Gradient background alternative (commented out):
            // ------------------------------------------------------------
            // // Create a rectangle that covers the same area as the table
            // // and apply a gradient fill using GradientAxialShading.
            // // This rectangle must be added to the page before the table
            // // so that the table appears on top of the gradient.
            // //
            // // GradientAxialShading gradient = new GradientAxialShading(Color.FromRgb(0.2, 0.6, 0.9),
            // //                                         Color.FromRgb(0.9, 0.9, 0.5));
            // // gradient.Start = new Point(0, 0);
            // // gradient.End   = new Point(0, page.PageInfo.Height);
            // //
            // // Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);
            // // Rectangle rect = new Rectangle(0, 0, page.PageInfo.Width, page.PageInfo.Height);
            // // rect.GraphInfo = new GraphInfo
            // // {
            // //     FillColor = gradient // This line will not compile because FillColor expects a Color.
            // // };
            // // graph.Shapes.Add(rect);
            // // // Insert the gradient rectangle before adding the table
            // // page.Paragraphs.Insert(0, graph);
            // //
            // // The above approach demonstrates the concept, but Aspose.Pdf does not
            // // currently support assigning a gradient directly to Table.BackgroundColor.
            // // Using a separate Graph with gradient shading is the recommended workaround.

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}