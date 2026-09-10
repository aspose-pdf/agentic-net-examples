using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a table and define three equal column widths
            Table table = new Table
            {
                // Column widths are specified as a space‑separated string (points)
                ColumnWidths = "150 150 150"
            };

            // Add a single row to the table
            Row row = table.Rows.Add();

            // Add a cell that will span three adjacent columns
            Cell mergedCell = row.Cells.Add("Merged Cell");
            mergedCell.ColSpan = 3; // Merge three columns into this cell

            // Optionally set some visual styling for clarity
            mergedCell.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Color.Black
            };
            mergedCell.BackgroundColor = Color.LightGray;

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF to a file
            doc.Save("MergedCellTable.pdf");
        }

        Console.WriteLine("PDF with a cell spanning three columns has been created.");
    }
}