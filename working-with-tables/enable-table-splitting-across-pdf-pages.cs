using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_split.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document (first page)
            Page page = doc.Pages.Add();

            // Create a table that will be added to the page
            Table table = new Table
            {
                // Enable splitting of the table across pages
                IsBroken = true,

                // Optional: set column widths (percentage of page width)
                ColumnWidths = "20 30 20 30"
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.DefaultCellTextState = new TextState { FontSize = 12, FontStyle = FontStyles.Bold };
            header.Cells.Add("ID");
            header.Cells.Add("Name");
            header.Cells.Add("Quantity");
            header.Cells.Add("Description");

            // Add many data rows to force the table to span multiple pages
            for (int i = 1; i <= 200; i++)
            {
                Row row = table.Rows.Add();
                row.DefaultCellTextState = new TextState { FontSize = 10 };
                row.Cells.Add(i.ToString());
                row.Cells.Add($"Item {i}");
                row.Cells.Add((i * 5).ToString());
                row.Cells.Add($"This is a description for item {i}. It may be long enough to wrap onto multiple lines.");
            }

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with split table saved to '{outputPath}'.");
    }
}