using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_fixed_width.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a table instance
            Table table = new Table();

            // Fix the table width to 500 points (total width)
            table.ColumnWidths = "500";

            // Optional: center the table on the page
            table.Alignment = HorizontalAlignment.Center;

            // Add a single row
            Row row = table.Rows.Add();

            // Add a cell to the row and insert some text
            Cell cell = row.Cells.Add();
            TextFragment tf = new TextFragment("Fixed width table cell");
            cell.Paragraphs.Add(tf);

            // Place the table on the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}