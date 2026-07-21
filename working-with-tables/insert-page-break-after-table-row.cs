using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add the first page where the table will be placed
            Page firstPage = doc.Pages.Add();

            // Create a table and add it to the page
            Table table = new Table();
            firstPage.Paragraphs.Add(table);

            // ----- Row 1 (appears on the first page) -----
            Row row1 = table.Rows.Add();               // Add a new row to the table
            row1.Cells.Add("Row 1 – stays on the first page.");

            // ----- Row 2 (should start on a new page) -----
            Row row2 = table.Rows.Add();               // Add the next row
            row2.IsInNewPage = true;                   // Force this row onto a new page
            row2.Cells.Add("Row 2 – begins on a new page.");

            // ----- Row 3 (continues on the new page) -----
            Row row3 = table.Rows.Add();
            row3.Cells.Add("Row 3 – follows Row 2 on the same new page.");

            // Save the resulting PDF
            doc.Save("NewPageFragmentAfterRow.pdf");
        }

        Console.WriteLine("PDF created with a page break after the specified row.");
    }
}