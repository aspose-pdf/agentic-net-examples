using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a table with one column
            Table table = new Table
            {
                ColumnWidths = "200" // width of the single column
            };

            // Add a row to the table
            Row row = table.Rows.Add();

            // Create a cell, set its background color, and add some text
            Cell cell = row.Cells.Add();
            cell.BackgroundColor = Aspose.Pdf.Color.LightGray; // set desired background color
            // Add a text fragment to the cell
            TextFragment tf = new TextFragment("Cell with gray background");
            cell.Paragraphs.Add(tf);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF to disk
            doc.Save("CellBackgroundColor.pdf");
        }

        Console.WriteLine("PDF with colored cell saved as 'CellBackgroundColor.pdf'.");
    }
}