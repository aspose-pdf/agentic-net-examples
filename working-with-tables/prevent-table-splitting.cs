using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_no_split.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a table and disable automatic splitting across pages
            Table table = new Table
            {
                IsBroken = false,               // Prevent the table from breaking onto the next page
                ColumnWidths = "100 100 100"    // Define three equal columns (optional)
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add(new TextFragment("Header 1"));
            header.Cells.Add(new TextFragment("Header 2"));
            header.Cells.Add(new TextFragment("Header 3"));

            // Add enough rows to exceed a single page height (to demonstrate the effect)
            for (int i = 1; i <= 30; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add(new TextFragment($"Row {i} Col 1"));
                row.Cells.Add(new TextFragment($"Row {i} Col 2"));
                row.Cells.Add(new TextFragment($"Row {i} Col 3"));
            }

            // Insert the table into the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'. The table will stay on one page.");
    }
}