using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // for BorderSide enum

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table
            Table table = new Table();

            // Configure the table border to have a thickness of 2 points and black color
            // BorderInfo does not expose settable Color or Width properties; they must be supplied via the constructor.
            table.Border = new BorderInfo(BorderSide.All, 2f, Aspose.Pdf.Color.Black);

            // Define column widths (optional, here two equal columns)
            table.ColumnWidths = "200 200";

            // Add a row with two cells
            Row row = table.Rows.Add();
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");

            // Add another row
            Row row2 = table.Rows.Add();
            row2.Cells.Add("Cell 3");
            row2.Cells.Add("Cell 4");

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF to a file
            doc.Save("TableWithBorder.pdf");
        }

        Console.WriteLine("PDF with table border (2 points) saved as 'TableWithBorder.pdf'.");
    }
}