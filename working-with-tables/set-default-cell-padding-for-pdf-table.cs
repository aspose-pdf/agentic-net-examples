using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_padding.pdf";

        // Ensure the Document is disposed properly
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a table and set the default cell padding for all cells
            Table table = new Table
            {
                // MarginInfo defines padding for Top, Bottom, Left, Right (in points)
                DefaultCellPadding = new MarginInfo
                {
                    Top = 5,
                    Bottom = 5,
                    Left = 5,
                    Right = 5
                }
            };

            // Optional: define column widths (in points or percentages)
            table.ColumnWidths = "100 100 100";

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Cell A1");
            data.Cells.Add("Cell A2");
            data.Cells.Add("Cell A3");

            // Place the table on the page
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}