using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_percent.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table instance
            Table table = new Table();

            // Set the table width to 80% of the page width.
            // By defining a single column width as "80%" the table occupies 80% of the page.
            table.ColumnWidths = "80%";

            // Optional: center the table on the page
            table.Alignment = HorizontalAlignment.Center;

            // Add a sample row and cell with some text
            Row row = table.Rows.Add();
            Cell cell = row.Cells.Add();
            cell.Paragraphs.Add(new TextFragment("Sample cell with 80% width"));

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}