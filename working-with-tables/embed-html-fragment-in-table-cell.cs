using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;                // For HtmlFragment (inherits from FormattedFragment)
using Aspose.Pdf.Facades;             // Not needed here but kept for completeness

class Program
{
    static void Main()
    {
        const string inputPath  = "template.pdf";   // optional existing PDF, can be empty
        const string outputPath = "table_with_html.pdf";

        // Ensure the input file exists; if not, create a blank document.
        if (!File.Exists(inputPath))
        {
            using (Document blank = new Document())
            {
                blank.Pages.Add();                 // add a single empty page
                blank.Save(inputPath);
            }
        }

        // Open the PDF (or blank template) inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Create a table and add it to the first page.
            Table table = new Table
            {
                ColumnWidths = "200"               // single column width
            };
            doc.Pages[1].Paragraphs.Add(table);

            // Add a row to the table.
            Row row = table.Rows.Add();

            // Add a cell to the row.
            Cell cell = row.Cells.Add();

            // Create an HtmlFragment with the desired markup.
            // Example markup: a simple styled paragraph with a link.
            string html = @"<p style='font-family:Arial; font-size:14pt; color:#0066CC;'>
                                This is <b>bold</b> and <i>italic</i> text with a 
                                <a href='https://www.example.com'>link</a>.
                            </p>";
            HtmlFragment htmlFragment = new HtmlFragment(html);

            // (Optional) customize loading options, e.g., set a base path for external resources.
            // htmlFragment.HtmlLoadOptions = new HtmlLoadOptions { BasePath = "resources/" };

            // Add the HtmlFragment to the cell's paragraph collection.
            cell.Paragraphs.Add(htmlFragment);

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with HTML fragment saved to '{outputPath}'.");
    }
}