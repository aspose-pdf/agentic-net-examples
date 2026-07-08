using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;                // For HtmlFragment (inherits FormattedFragment)

class Program
{
    static void Main()
    {
        // Paths for input (optional) and output PDF
        const string outputPath = "HtmlInCell.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and define column widths (two columns for demonstration)
            Table table = new Table
            {
                ColumnWidths = "200 200"
            };

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add the first cell (plain text)
            Cell cell1 = row.Cells.Add("Plain text cell");

            // Add the second cell where we will embed HTML markup
            Cell cell2 = row.Cells.Add();

            // Create an HtmlFragment with the desired HTML markup
            // Example: bold text with a hyperlink
            string html = "<b>Bold Text</b> and a <a href=\"https://www.example.com\">link</a>";
            HtmlFragment htmlFragment = new HtmlFragment(html);

            // Optionally, set HtmlLoadOptions if you need custom base path or resource handling
            // htmlFragment.HtmlLoadOptions = new HtmlLoadOptions { BasePath = "path/to/resources" };

            // Add the HtmlFragment to the cell's paragraph collection
            // The Paragraphs property holds BaseParagraph objects, and HtmlFragment derives from FormattedFragment -> BaseParagraph
            cell2.Paragraphs.Add(htmlFragment);

            // Ensure the cell respects the fragment's text state (optional but recommended)
            cell2.IsOverrideByFragment = true;

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF document (no SaveOptions needed for PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with HTML fragment saved to '{outputPath}'.");
    }
}