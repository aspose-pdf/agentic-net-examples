using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;          // TextState, TextFragment, FontRepository
using Aspose.Pdf.Drawing;       // BorderInfo, BorderSide, MarginInfo

class TableStylingExample
{
    static void Main()
    {
        // Paths (adjust as needed)
        const string outputPath = "styled_table.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // ------------------------------------------------------------
            // Create a Table and configure its overall appearance
            // ------------------------------------------------------------
            Table table = new Table
            {
                // Table background
                BackgroundColor = Aspose.Pdf.Color.LightGray,

                // Include the border width in column calculations
                IsBordersIncluded = true,

                // Default cell padding (5 points on all sides)
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5),

                // Default text formatting for cells
                DefaultCellTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12,
                    ForegroundColor = Aspose.Pdf.Color.Blue
                }
            };

            // Table border (solid black, 1 point)
            table.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black);

            // Default cell border (thin dark gray)
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.DarkGray);

            // ------------------------------------------------------------
            // Add a header row with custom styling
            // ------------------------------------------------------------
            Row headerRow = table.Rows.Add();
            headerRow.BackgroundColor = Aspose.Pdf.Color.Gray; // header background

            // First header cell
            Cell headerCell1 = headerRow.Cells.Add();
            headerCell1.Paragraphs.Add(new TextFragment("Product"));
            // Override text style for this cell (bold)
            headerCell1.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Bold"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.White
            };
            // Cell border (solid red)
            headerCell1.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Red);

            // Second header cell
            Cell headerCell2 = headerRow.Cells.Add();
            headerCell2.Paragraphs.Add(new TextFragment("Price"));
            headerCell2.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Bold"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.White
            };
            headerCell2.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Red);

            // ------------------------------------------------------------
            // Add data rows with alternating background colors
            // ------------------------------------------------------------
            string[,] data = {
                { "Widget A", "$10.00" },
                { "Widget B", "$15.50" },
                { "Widget C", "$7.25" }
            };

            for (int i = 0; i < data.GetLength(0); i++)
            {
                Row dataRow = table.Rows.Add();

                // Alternate row background
                dataRow.BackgroundColor = (i % 2 == 0) ? Aspose.Pdf.Color.White : Aspose.Pdf.Color.AliceBlue;

                // First column cell
                Cell cell1 = dataRow.Cells.Add();
                cell1.Paragraphs.Add(new TextFragment(data[i, 0]));

                // Second column cell
                Cell cell2 = dataRow.Cells.Add();
                cell2.Paragraphs.Add(new TextFragment(data[i, 1]));
                // Right‑align price values
                cell2.Alignment = HorizontalAlignment.Right;
            }

            // ------------------------------------------------------------
            // Position the table on the page (optional)
            // ------------------------------------------------------------
            table.Left = 50;   // distance from left edge
            table.Top = 700;   // distance from bottom edge

            // Add the styled table to the page
            page.Paragraphs.Add(table);

            // Save the document (lifecycle rule: use doc.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with styled table saved to '{outputPath}'.");
    }
}
