using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_fixed_width.pdf";

        // Ensure the Document is disposed properly
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a table with a total width of 500 points.
            // ColumnWidths expects a space‑separated string, not a float array.
            Table table = new Table
            {
                ColumnWidths = "250 250",
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            foreach (Cell cell in header.Cells)
                cell.BackgroundColor = Color.LightGray;

            // Sample data rows
            for (int i = 1; i <= 3; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Item {i}");
                row.Cells.Add($"Value {i * 10}");
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF (PDF format by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with fixed-width table saved to '{outputPath}'.");
    }
}
