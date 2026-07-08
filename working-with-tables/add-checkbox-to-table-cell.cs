using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "checkbox_in_table.pdf";

        // Create a new PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and add it to the page
            Table table = new Table
            {
                // Optional: set table border for visibility
                Border = new BorderInfo(BorderSide.All, 1, Color.Black),
                // Define a single column width (100 points)
                ColumnWidths = "100"
            };
            page.Paragraphs.Add(table);

            // Add a row to the table and set its height
            Row row = table.Rows.Add();
            row.FixedRowHeight = 30; // height of the row (and thus the cell)

            // Add a cell – content is not required for the checkbox
            Cell cell = row.Cells.Add();

            // Calculate a rectangle that fits inside the cell.
            // The table starts at the default margin (0,0) of the page.
            // For simplicity we place the checkbox 5 points from the left/top of the cell.
            // Page coordinate system: origin (0,0) is bottom‑left.
            double pageHeight = page.PageInfo.Height;
            double cellX = 0;                     // left edge of the first column
            double cellY = pageHeight - row.FixedRowHeight; // top edge of the row
            double checkboxLeft = cellX + 5;
            double checkboxBottom = cellY - row.FixedRowHeight + 5;
            double checkboxRight = checkboxLeft + 20; // 20‑point wide checkbox
            double checkboxTop = checkboxBottom + 20; // 20‑point high checkbox

            // Fully qualify the rectangle type to avoid ambiguity (Aspose.Pdf.Rectangle is required for form fields)
            Aspose.Pdf.Rectangle checkboxRect = new Aspose.Pdf.Rectangle(
                checkboxLeft, checkboxBottom, checkboxRight, checkboxTop);

            // Create a checkbox form field inside the calculated rectangle
            CheckboxField checkbox = new CheckboxField(page, checkboxRect)
            {
                Name = "SampleCheckBox",
                ExportValue = "Checked",
                Checked = false,
                Style = BoxStyle.Check
            };

            // Add the checkbox field to the document's form collection
            doc.Form.Add(checkbox);

            // Save the PDF (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with checkbox saved to '{outputPath}'.");
    }
}
