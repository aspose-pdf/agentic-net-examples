using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // HtmlFragment resides here

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_html.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with a single column (width 200 points)
            Table table = new Table
            {
                ColumnWidths = "200"
            };
            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // Embed HTML markup inside the cell using HtmlFragment
            HtmlFragment html = new HtmlFragment(
                "<p>This is <b>bold</b> and <i>italic</i> text inside a table cell.</p>"
            );
            cell.Paragraphs.Add(html);

            // Optional: add a visible border to the cell for demonstration
            cell.Border = new BorderInfo(BorderSide.All, 1f);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}