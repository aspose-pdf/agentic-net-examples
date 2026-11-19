using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "table_width_percent.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Calculate column widths so that the whole table occupies 80% of the page width
            double pageWidth = page.PageInfo.Width;               // total page width in points
            double columnWidth = pageWidth * 0.4;                 // two columns => 40% each = 80% total

            // Create a table and set its column widths accordingly
            Table table = new Table
            {
                ColumnWidths = $"{columnWidth} {columnWidth}" // each column gets 40% of page width
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Data 1");
            data.Cells.Add("Data 2");

            // Insert the table into the page's content
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
