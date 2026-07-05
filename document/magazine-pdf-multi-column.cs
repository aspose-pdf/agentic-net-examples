using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class MagazinePdf
{
    static void Main()
    {
        const string outputPath = "magazine.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (A4 size)
            Page page = doc.Pages.Add();
            // Set page size via PageInfo (recommended for newer versions)
            page.PageInfo.Width = PageSize.A4.Width;
            page.PageInfo.Height = PageSize.A4.Height;

            // ------------------------------------------------------------
            // Multi‑column layout (implemented with a Table because the
            // Page.ColumnInfo API is not available in the current Aspose.Pdf version)
            // ------------------------------------------------------------
            // Create a two‑column table that spans the page width. The article text
            // will flow inside the first column; the second column is left empty so
            // the text automatically wraps to it when the first column is filled.
            var table = new Table();
            // Define column widths (half of the page width minus a small gap)
            double columnGap = 20.0; // points between columns
            double columnWidth = (page.PageInfo.Width - columnGap) / 2.0;
            table.ColumnWidths = $"{columnWidth} {columnWidth}";
            // Remove borders for a clean magazine look
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0);

            // Add a single row with two cells
            var row = table.Rows.Add();
            // First cell – article text (will wrap and continue into the second cell)
            var articleCell = new Cell();
            string article = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                             "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                             "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris " +
                             "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in " +
                             "reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla " +
                             "pariatur. Excepteur sint occaecat cupidatat non proident, sunt in " +
                             "culpa qui officia deserunt mollit anim id est laborum.";
            var tf = new TextFragment(article);
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 12;
            tf.TextState.ForegroundColor = Color.Black;
            articleCell.Paragraphs.Add(tf);
            row.Cells.Add(articleCell);

            // Second cell – empty (acts as the continuation column)
            var emptyCell = new Cell();
            row.Cells.Add(emptyCell);

            // Add the table to the page – Aspose.Pdf will flow the text from the first
            // cell into the second when the first column is filled, achieving a magazine‑style layout.
            page.Paragraphs.Add(table);

            // ------------------------------------------------------------
            // OPTIONAL: Add an advertisement image spanning the full page width at the top
            // ------------------------------------------------------------
            string adImagePath = "adImage.jpg";
            if (File.Exists(adImagePath))
            {
                // Define a rectangle covering the top 100 points of the page (float values are required)
                float pageWidth = (float)page.PageInfo.Width;
                float adHeight = 100f;
                var adRect = new Aspose.Pdf.Rectangle(
                    0f,                                          // llx
                    (float)page.PageInfo.Height - adHeight,      // lly (from bottom)
                    pageWidth,                                   // urx
                    (float)page.PageInfo.Height);                // ury (top)

                page.AddImage(adImagePath, adRect);
            }

            // ------------------------------------------------------------
            // OPTIONAL: Add a decorative banner shape behind the ad using Graph
            // ------------------------------------------------------------
            // Use the double‑based Graph constructor as the float‑based one is obsolete.
            var graph = new Graph(500.0, 100.0); // width, height of the shape container

            // Rectangle shape (float parameters) with a light‑gray fill and dark‑gray border
            var shapeRect = new Aspose.Pdf.Drawing.Rectangle(
                0f,   // llx
                0f,   // lly
                500f, // urx (right edge)
                100f  // ury (top edge)
            );
            shapeRect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.DarkGray,
                LineWidth = 1f
            };
            graph.Shapes.Add(shapeRect);

            // Position the banner at the top of the page by adding a new paragraph.
            // The graph will appear at the current cursor location, which is the top of the page after the optional image.
            page.Paragraphs.Add(graph);

            // Save the final PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Magazine PDF saved to '{outputPath}'.");
    }
}
