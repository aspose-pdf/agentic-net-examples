using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // for BorderInfo and BorderSide

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_border.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a visual Table object
            Table table = new Table();

            // Set column widths (example: two columns of equal width)
            table.ColumnWidths = "200 200";

            // Configure the border thickness to 2 points for all cells
            // Use DefaultCellBorder which accepts a BorderInfo constructed with side and width
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 2f);

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Cell A");
            data.Cells.Add("Cell B");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF (Document.Save writes PDF regardless of extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table border saved to '{outputPath}'.");
    }
}
