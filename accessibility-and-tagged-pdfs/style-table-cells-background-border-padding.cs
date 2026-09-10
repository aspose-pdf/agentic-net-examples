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

            // Create a table and set column widths (two columns of equal width)
            Table table = new Table
            {
                ColumnWidths = "200 200", // width in points
                // Optional: set a border for the whole table
                Border = new BorderInfo(BorderSide.All, 1, Aspose.Pdf.Color.Black),
                // Set default padding for all cells (can be overridden per cell)
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // ---------- First Row ----------
            Row row1 = table.Rows.Add();

            // Cell (1,1)
            Cell cell11 = row1.Cells.Add();
            cell11.Paragraphs.Add(new TextFragment("Cell 1"));
            cell11.BackgroundColor = Aspose.Pdf.Color.LightYellow;                     // Background color
            cell11.Border = new BorderInfo(BorderSide.All, 2, Aspose.Pdf.Color.Red);   // Border thickness & color
            cell11.Margin = new MarginInfo(4, 4, 4, 4);                                 // Padding (left, right, top, bottom)

            // Cell (1,2)
            Cell cell12 = row1.Cells.Add();
            cell12.Paragraphs.Add(new TextFragment("Cell 2"));
            cell12.BackgroundColor = Aspose.Pdf.Color.LightGreen;
            cell12.Border = new BorderInfo(BorderSide.All, 2, Aspose.Pdf.Color.Blue);
            cell12.Margin = new MarginInfo(4, 4, 4, 4);

            // ---------- Second Row ----------
            Row row2 = table.Rows.Add();

            // Cell (2,1)
            Cell cell21 = row2.Cells.Add();
            cell21.Paragraphs.Add(new TextFragment("Cell 3"));
            cell21.BackgroundColor = Aspose.Pdf.Color.LightCyan;
            cell21.Border = new BorderInfo(BorderSide.All, 2, Aspose.Pdf.Color.DarkGray);
            cell21.Margin = new MarginInfo(4, 4, 4, 4);

            // Cell (2,2)
            Cell cell22 = row2.Cells.Add();
            cell22.Paragraphs.Add(new TextFragment("Cell 4"));
            cell22.BackgroundColor = Aspose.Pdf.Color.LightPink;
            cell22.Border = new BorderInfo(BorderSide.All, 2, Aspose.Pdf.Color.Purple);
            cell22.Margin = new MarginInfo(4, 4, 4, 4);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save("styled_table.pdf");
        }

        Console.WriteLine("PDF with styled table cells saved as 'styled_table.pdf'.");
    }
}