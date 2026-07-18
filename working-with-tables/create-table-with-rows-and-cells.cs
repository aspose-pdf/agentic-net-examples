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

            // Create a Table object
            Table table = new Table();

            // Optional: set column widths (example: three equal columns)
            table.ColumnWidths = "100 100 100";

            // Add first row
            Row row1 = table.Rows.Add();               // Add a new Row to the table
            row1.Cells.Add("Header 1");                // Add cells to the row
            row1.Cells.Add("Header 2");
            row1.Cells.Add("Header 3");

            // Add second row
            Row row2 = table.Rows.Add();
            row2.Cells.Add("Row 1, Col 1");
            row2.Cells.Add("Row 1, Col 2");
            row2.Cells.Add("Row 1, Col 3");

            // Add third row
            Row row3 = table.Rows.Add();
            row3.Cells.Add("Row 2, Col 1");
            row3.Cells.Add("Row 2, Col 2");
            row3.Cells.Add("Row 2, Col 3");

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF to a file
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with table created successfully.");
    }
}