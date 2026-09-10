using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_rowspan.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with three equal-width columns and a thin black border
            Table table = new Table
            {
                ColumnWidths = "100 100 100",
                Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black) // float literal for width
            };
            page.Paragraphs.Add(table);

            // First row
            Row row1 = table.Rows.Add();
            // Add three cells to the first row
            Cell cellA = row1.Cells.Add("A1");
            Cell cellB = row1.Cells.Add("B1");
            Cell cellC = row1.Cells.Add("C1");

            // Merge cellA with the cell directly below by setting RowSpan to 2
            cellA.RowSpan = 2;

            // Second row (cellA is spanning, so only two cells are needed here)
            Row row2 = table.Rows.Add();
            Cell cellB2 = row2.Cells.Add("B2");
            Cell cellC2 = row2.Cells.Add("C2");

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}