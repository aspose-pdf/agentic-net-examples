using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string outputPath = "table_80percent.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table (inherits BaseParagraph)
            Table table = new Table();

            // Set the table width to 80% of the page width.
            // ColumnWidths accepts percentage values; using a single column with "80%" makes the whole table 80% wide.
            table.ColumnWidths = "80%";

            // Optional: set alignment to center so the table is centered on the page
            table.Alignment = HorizontalAlignment.Center;

            // Add a simple row with a cell for demonstration
            Row row = table.Rows.Add();
            Cell cell = row.Cells.Add();
            // Add some text to the cell
            TextFragment tf = new TextFragment("Sample content inside an 80% width table.");
            cell.Paragraphs.Add(tf);

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with 80% width table saved to '{outputPath}'.");
    }
}