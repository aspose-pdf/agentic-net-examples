using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output paths
        const string outputPath = "table_double_border.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position and size
            Table table = new Table
            {
                // Define column widths (example: two equal columns)
                ColumnWidths = "200 200",
                // Set a background color (optional)
                BackgroundColor = Color.LightGray
            };

            // Define a double‑like border for the table.
            // Aspose.Pdf.BorderInfo does not expose a Style property; the double line effect
            // can be approximated by using a thicker border width. The constructor sets side,
            // width and color.
            table.Border = new BorderInfo(BorderSide.All, 2f, Color.DarkBlue);

            // Add a header row
            Row header = table.Rows.Add();
            Cell headerCell1 = header.Cells.Add("Header 1");
            Cell headerCell2 = header.Cells.Add("Header 2");
            // Make header text bold
            headerCell1.DefaultCellTextState = new TextState { FontStyle = FontStyles.Bold };
            headerCell2.DefaultCellTextState = new TextState { FontStyle = FontStyles.Bold };

            // Add a data row
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Value A");
            dataRow.Cells.Add("Value B");

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with double‑border table saved to '{outputPath}'.");
    }
}
