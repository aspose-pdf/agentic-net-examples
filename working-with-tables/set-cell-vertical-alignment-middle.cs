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

            // Create a table with one column
            Table table = new Table
            {
                ColumnWidths = "200" // width of the single column
            };

            // Add a row to the table
            Row row = table.Rows.Add();

            // Create a cell and set its vertical alignment to Middle (Center)
            Cell cell = new Cell();
            cell.VerticalAlignment = VerticalAlignment.Center; // Middle alignment

            // Add some text to the cell
            TextFragment tf = new TextFragment("Centered vertically");
            tf.TextState.FontSize = 12;
            cell.Paragraphs.Add(tf);

            // Add the cell to the row
            row.Cells.Add(cell);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save("CellVerticalAlignment_Middle.pdf");
        }

        Console.WriteLine("PDF created with cell vertical alignment set to Middle.");
    }
}