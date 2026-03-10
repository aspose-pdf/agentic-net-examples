using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_output.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // Create and configure a Table object
            // -------------------------------------------------
            Table table = new Table();

            // Set a border around the whole table (BorderInfo requires side, width, and color)
            table.Border = new BorderInfo(BorderSide.All, 1.0f, Color.Black);

            // Define default cell border and padding
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Black);
            table.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);

            // Set default text appearance for all cells
            table.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Color.Black
            };

            // Define column widths (values are in points)
            table.ColumnWidths = "100 150 100";

            // -------------------------------------------------
            // Populate the table with rows and cells
            // -------------------------------------------------
            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // First data row
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Row1 Col1");
            row1.Cells.Add("Row1 Col2");
            row1.Cells.Add("Row1 Col3");

            // Second data row
            Row row2 = table.Rows.Add();
            row2.Cells.Add("Row2 Col1");
            row2.Cells.Add("Row2 Col2");
            row2.Cells.Add("Row2 Col3");

            // -------------------------------------------------
            // Insert the table into the page's paragraph collection
            // -------------------------------------------------
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }
}
