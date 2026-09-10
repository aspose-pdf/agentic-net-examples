using System;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for HtmlFragment (inherits FormattedFragment)

class Program
{
    static void Main()
    {
        // Input and output paths
        const string outputPath = "TableWithHtmlCell.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page (first page is at index 1)
            Page page = doc.Pages.Add();

            // Create a table and add it to the page
            Table table = new Table
            {
                // Optional: set table border and column widths
                Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black),
                ColumnWidths = "200 200"
            };
            page.Paragraphs.Add(table);

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // Create an HtmlFragment with the desired markup
            HtmlFragment html = new HtmlFragment("<b>Hello <i>World</i> from <u>HTML</u></b>");

            // Optionally, set HtmlLoadOptions if you need custom settings (e.g., BasePath)
            // html.HtmlLoadOptions = new HtmlLoadOptions { BasePath = "path/to/resources" };

            // Add the HtmlFragment to the cell's paragraph collection
            cell.Paragraphs.Add(html);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}