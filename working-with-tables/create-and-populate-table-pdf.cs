using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_output.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a visual Table object
            Table table = new Table();

            // Optional: define column widths (three columns, each 100 points wide)
            table.ColumnWidths = "100 100 100";

            // Optional: set default cell appearance (border, padding, text style)
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f);
            table.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);
            table.DefaultCellTextState = new TextState
            {
                FontSize = 12,
                Font = FontRepository.FindFont("Helvetica")
            };

            // ----- Header Row -----
            Row header = table.Rows.Add();               // Add a new row
            header.Cells.Add("Header 1");                // Add cells to the row
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // ----- First Data Row -----
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Row1 Col1");
            row1.Cells.Add("Row1 Col2");
            row1.Cells.Add("Row1 Col3");

            // ----- Second Data Row -----
            Row row2 = table.Rows.Add();
            row2.Cells.Add("Row2 Col1");
            row2.Cells.Add("Row2 Col2");
            row2.Cells.Add("Row2 Col3");

            // Add the completed table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document to disk
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }
}