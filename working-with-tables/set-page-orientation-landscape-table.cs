using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // Open the sample PDF and modify it
        using (Document doc = new Document("input.pdf"))
        {
            // Set page orientation to landscape
            Page page = doc.Pages[1];
            page.PageInfo.IsLandscape = true;
            // Adjust page size to A4 landscape (842 x 595 points)
            page.SetPageSize(842, 595);

            // Create a wide table
            Table table = new Table();
            table.ColumnWidths = "200 200 200"; // three wide columns
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f);

            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // First data row
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Cell 1");
            row1.Cells.Add("Cell 2");
            row1.Cells.Add("Cell 3");

            // Second data row
            Row row2 = table.Rows.Add();
            row2.Cells.Add("Cell 4");
            row2.Cells.Add("Cell 5");
            row2.Cells.Add("Cell 6");

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save("output.pdf");
        }
    }
}