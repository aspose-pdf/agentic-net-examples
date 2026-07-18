using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;                // For text handling (optional)

class Program
{
    static void Main()
    {
        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a table with 3 columns
            Table table = new Table
            {
                // Apply a solid black border to the entire table
                Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black),

                // Optional: include border width in column calculations
                IsBordersIncluded = true
            };

            // Define column widths (percentage of the page width)
            table.ColumnWidths = "33 33 34";

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Cell A1");
            data.Cells.Add("Cell A2");
            data.Cells.Add("Cell A3");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF to disk
            doc.Save("TableWithBorder.pdf");
        }

        Console.WriteLine("PDF with bordered table created successfully.");
    }
}