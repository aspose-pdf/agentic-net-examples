using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // FloatingBox, Color, BorderInfo, MarginInfo

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_shadow.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // ------------------------------------------------------------
            // Build the table using the core Table API (no Rect, no ShadowEffect)
            // ------------------------------------------------------------
            Table table = new Table
            {
                ColumnWidths = "30% 70%",
                // Optional visual styling
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Black),
                DefaultCellPadding = new MarginInfo(4, 4, 4, 4)
            };

            Row row = table.Rows.Add();
            row.Cells.Add("Header");
            row.Cells.Add("This is a sample table with a shadow effect.");

            // ------------------------------------------------------------
            // Simulate a shadow by drawing a semi‑transparent box behind the table
            // ------------------------------------------------------------
            // Desired table position (left, top) in points – same as the original Rect
            const float tableLeft = 50f;
            const float tableTop = 750f;
            // Shadow offset
            const float offsetX = 5f;
            const float offsetY = -5f; // negative moves the shadow downwards

            // Approximate the table size. For a simple table we can use the page width minus margins.
            // Here we use the width derived from the page (right‑left = 500 points) and a modest height.
            float tableWidth = 500f; // 550 - 50 as in the original Rect
            float tableHeight = (float)table.GetHeight(page); // let Aspose calculate the height
            if (tableHeight < 20f) tableHeight = 50f; // fallback for very small tables

            // Shadow box (drawn first so it appears behind the table)
            var shadowBox = new FloatingBox(tableWidth, tableHeight)
            {
                PositioningMode = ParagraphPositioningMode.Absolute,
                Left = tableLeft + offsetX,
                Top = tableTop + offsetY,
                BackgroundColor = Color.FromArgb(80, 0, 0, 0) // semi‑transparent black
            };

            // Table container box (white background to hide the shadow edges)
            var tableBox = new FloatingBox(tableWidth, tableHeight)
            {
                PositioningMode = ParagraphPositioningMode.Absolute,
                Left = tableLeft,
                Top = tableTop,
                BackgroundColor = Color.White
            };
            tableBox.Paragraphs.Add(table);

            // Add the shadow first, then the table box
            page.Paragraphs.Add(shadowBox);
            page.Paragraphs.Add(tableBox);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table shadow saved to '{outputPath}'.");
    }
}
