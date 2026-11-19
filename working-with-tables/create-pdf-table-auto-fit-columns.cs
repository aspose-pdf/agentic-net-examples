using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "autoFitTable.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table. No fixed column widths are set because we will auto‑fit later.
            Table table = new Table
            {
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("ID");
            header.Cells.Add("Name");
            header.Cells.Add("Description");

            // Populate the table with sample data rows
            for (int i = 1; i <= 10; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add(i.ToString());
                row.Cells.Add($"Item {i}");
                row.Cells.Add($"This is a longer description for item {i} that may require column auto‑fit.");
            }

            // Adjust column widths automatically based on the content
            table.ColumnAdjustment = ColumnAdjustment.AutoFitToContent;

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
