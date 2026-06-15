using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a sample PDF file (self‑contained example)
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // Open the sample PDF and add a table with a double‑like border (solid with width)
        using (Document pdfDoc = new Document("input.pdf"))
        {
            // Create a table with two columns
            Table table = new Table();
            table.ColumnWidths = "200 200";

            // Add a single row
            Row row = table.Rows.Add();

            // Add two cells to the row
            Cell cell1 = row.Cells.Add("Cell 1");
            Cell cell2 = row.Cells.Add("Cell 2");

            // Define a border with a width of 2 points.
            // Aspose.Pdf.BorderInfo does not expose a Style property; the double style is not supported.
            // Using a solid border with the required width achieves a similar visual effect.
            BorderInfo borderInfo = new BorderInfo(BorderSide.All, 2.0f);
            table.Border = borderInfo;

            // Add the table to the first page (1‑based indexing)
            pdfDoc.Pages[1].Paragraphs.Add(table);

            // Save the resulting PDF
            pdfDoc.Save("output.pdf");
        }
    }
}