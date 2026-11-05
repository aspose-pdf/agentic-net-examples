using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "multi_page_table.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page; additional pages will be created automatically when the table overflows
            Page page = doc.Pages.Add();

            // Create a table element
            Table table = new Table
            {
                // Define column widths as percentages (optional but improves layout)
                ColumnWidths = "15% 35% 35% 15%"
            };

            // Set the first row to repeat as a header on each new page
            table.RepeatingRowsCount = 1;

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("ID");
            header.Cells.Add("Name");
            header.Cells.Add("Description");
            header.Cells.Add("Value");

            // Populate the table with enough rows to span multiple pages
            for (int i = 1; i <= 200; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add(i.ToString());
                row.Cells.Add($"Item {i}");
                row.Cells.Add($"Detailed description for item {i}, which may wrap onto multiple lines.");
                row.Cells.Add((i * 10).ToString());
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multi‑page table saved to '{outputPath}'.");
    }
}
