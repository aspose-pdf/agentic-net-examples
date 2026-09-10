using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_fixed_width.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table instance
            Table table = new Table();

            // Set the table's total width to 500 points.
            // ColumnWidths expects a string; a single value defines the overall width.
            table.ColumnWidths = "500";

            // Optional: center the table on the page
            table.Alignment = HorizontalAlignment.Center;

            // Add a single row with one cell containing some text
            Row row = table.Rows.Add();
            Cell cell = row.Cells.Add();
            cell.Paragraphs.Add(new TextFragment("This table has a fixed width of 500 points."));

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF to the specified file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with fixed-width table saved to '{outputPath}'.");
    }
}