using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "custom_row_height.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with three columns of equal width
            Table table = new Table
            {
                ColumnWidths = "100 100 100"
            };

            // Add a header row and set its custom height (40 points)
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");
            header.FixedRowHeight = 40; // Fixed point value for row height

            // Add a second row and set a different custom height (30 points)
            Row row2 = table.Rows.Add();
            row2.Cells.Add("Cell 1");
            row2.Cells.Add("Cell 2");
            row2.Cells.Add("Cell 3");
            row2.FixedRowHeight = 30; // Fixed point value for row height

            // Add a third row without custom height (uses default)
            Row row3 = table.Rows.Add();
            row3.Cells.Add("Data 1");
            row3.Cells.Add("Data 2");
            row3.Cells.Add("Data 3");

            // Insert the table into the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
