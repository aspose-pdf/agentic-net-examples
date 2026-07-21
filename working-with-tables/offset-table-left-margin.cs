using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a table
            Table table = new Table();

            // Set the left offset using the Margin property (points)
            table.Margin = new MarginInfo { Left = 50 };

            // Define column widths
            table.ColumnWidths = "100 100 100";

            // Add a row
            Row row = new Row();
            table.Rows.Add(row);

            // Add cells to the created row
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");
            row.Cells.Add("Cell 3");

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            string outputPath = "TableOffset.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
    }
}