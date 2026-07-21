using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and add it to the page
            Table table = new Table();
            page.Paragraphs.Add(table);

            // Add two rows to the table
            Row row1 = table.Rows.Add();
            Row row2 = table.Rows.Add();

            // Add a cell to the first row and set its RowSpan to 2
            // This merges the cell with the cell directly below it
            Cell mergedCell = row1.Cells.Add("Merged Cell");
            mergedCell.RowSpan = 2; // Merge with the cell below

            // Add remaining cells to complete the table layout
            row1.Cells.Add("Cell 1-2");
            row2.Cells.Add("Cell 2-2");

            // Save the PDF to a file
            string outputPath = "TableWithRowSpan.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
    }
}