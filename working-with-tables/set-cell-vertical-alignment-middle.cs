using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;                // For text fragments (if needed)

class SetCellVerticalAlignment
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with 2 columns and 2 rows
            Table table = new Table
            {
                ColumnWidths = "200 200",   // Define column widths
                Border = new BorderInfo(BorderSide.All, 1, Color.Black)
            };
            page.Paragraphs.Add(table);

            // ---- First row ----
            Row row1 = table.Rows.Add();

            // First cell: set vertical alignment to Middle (Center)
            Cell cell11 = new Cell
            {
                // Add some text to the cell
                Paragraphs = { new TextFragment("Top") },
                // Align content vertically to the middle of the cell
                VerticalAlignment = VerticalAlignment.Center
            };
            row1.Cells.Add(cell11);

            // Second cell: also middle-aligned
            Cell cell12 = new Cell
            {
                Paragraphs = { new TextFragment("Bottom") },
                VerticalAlignment = VerticalAlignment.Center
            };
            row1.Cells.Add(cell12);

            // ---- Second row ----
            Row row2 = table.Rows.Add();

            Cell cell21 = new Cell
            {
                Paragraphs = { new TextFragment("Middle") },
                VerticalAlignment = VerticalAlignment.Center
            };
            row2.Cells.Add(cell21);

            Cell cell22 = new Cell
            {
                Paragraphs = { new TextFragment("Middle") },
                VerticalAlignment = VerticalAlignment.Center
            };
            row2.Cells.Add(cell22);

            // Save the PDF to a file
            const string outputPath = "CellVerticalAlignment_Middle.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
    }
}