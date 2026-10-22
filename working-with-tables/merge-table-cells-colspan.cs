using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "TableWithColSpan.pdf";

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a table with three columns of equal width
            Table table = new Table
            {
                ColumnWidths = "100 100 100"
            };

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // ----- Header row: one cell spanning three columns -----
            Row headerRow = table.Rows.Add();
            Cell headerCell = headerRow.Cells.Add("Header spanning three columns");
            headerCell.ColSpan = 3; // Merge three adjacent columns
            // Set a light gray background using Aspose.Pdf.Color (fully qualified)
            headerCell.BackgroundColor = Aspose.Pdf.Color.LightGray;

            // ----- Data row: three separate cells -----
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Cell 1");
            dataRow.Cells.Add("Cell 2");
            dataRow.Cells.Add("Cell 3");

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}