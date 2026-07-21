using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "checkbox_in_table.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and add it to the page
            Table table = new Table();
            page.Paragraphs.Add(table);

            // Define column widths (single column of 100 points)
            table.ColumnWidths = "100";

            // Add a row and set its height – this determines the cell height
            Row row = table.Rows.Add();
            row.FixedRowHeight = 30; // 30 points height

            // Add an empty cell that will host the checkbox
            Cell cell = row.Cells.Add(string.Empty);

            // Calculate the rectangle where the checkbox will be placed.
            // The rectangle is defined in PDF user space (lower‑left origin).
            // We place the checkbox a few points inside the cell for visual padding.
            // Cell position: X = table's left margin (0) + column offset (0)
            //                Y = page height - top margin - row index * row height
            double pageHeight = page.PageInfo.Height;
            double cellX = 0;                     // left edge of the table
            double cellY = pageHeight - row.FixedRowHeight; // top of the first row
            double padding = 5; // inner padding inside the cell
            Aspose.Pdf.Rectangle checkboxRect = new Aspose.Pdf.Rectangle(
                cellX + padding,
                cellY - row.FixedRowHeight + padding,
                cellX + 15 + padding,
                cellY - padding);

            // Create a checkbox form field positioned inside the calculated rectangle
            CheckboxField checkbox = new CheckboxField(page, checkboxRect)
            {
                Checked = true,
                ExportValue = "Yes",
                Color = Aspose.Pdf.Color.Blue,
                Style = BoxStyle.Check // default style
            };

            // Add the checkbox field to the document's form collection
            doc.Form.Add(checkbox);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with checkbox saved to '{outputPath}'.");
    }
}
