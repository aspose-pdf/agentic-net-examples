using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "colspan_table.pdf";

        // Create a new PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document())
        {
            // Add a blank page to host the table
            Page page = doc.Pages.Add();

            // Define a table with three equal-width columns
            Table table = new Table
            {
                ColumnWidths = "100 100 100"
            };
            page.Paragraphs.Add(table); // attach table to the page

            // First row: a single cell that spans all three columns
            Row mergedRow = table.Rows.Add();
            Cell mergedCell = mergedRow.Cells.Add("Merged Cell");
            mergedCell.ColSpan = 3; // merge three adjacent columns into one cell

            // Second row: three regular cells for visual reference
            Row normalRow = table.Rows.Add();
            normalRow.Cells.Add("Cell 1");
            normalRow.Cells.Add("Cell 2");
            normalRow.Cells.Add("Cell 3");

            // Save the PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with a colspan cell saved to '{outputPath}'.");
    }
}