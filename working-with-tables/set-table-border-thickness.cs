using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_border.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            Table table = new Table();

            // Set the table border thickness to 2 points.
            table.Border = new BorderInfo(BorderSide.All, 2f);

            // Define column widths (three columns).
            table.ColumnWidths = "100 150 100";

            // Uniform cell padding of 5 points on all sides.
            table.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);

            // Add a header row.
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add a data row.
            Row data = table.Rows.Add();
            data.Cells.Add("Cell A1");
            data.Cells.Add("Cell A2");
            data.Cells.Add("Cell A3");

            // Add the table to the page.
            page.Paragraphs.Add(table);

            // Save the PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
