using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Ensure the Document is disposed properly (lifecycle rule)
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a simple table with one row and two cells
            Table table = new Table();
            table.ColumnWidths = "200 200"; // Define column widths

            // Add a single row and populate its cells
            var row = table.Rows.Add();
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Check whether the table is set to break across pages
            bool isBroken = table.IsBroken; // Property from Table.IsBroken
            Console.WriteLine($"Table IsBroken: {isBroken}");

            // (Optional) Save the document to verify the result
            doc.Save("output.pdf");
        }
    }
}