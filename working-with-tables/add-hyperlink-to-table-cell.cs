using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
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

            // Create a table with one column
            Table table = new Table
            {
                ColumnWidths = "200"
            };
            page.Paragraphs.Add(table);

            // Add a row to the table and set a fixed height (required for rectangle calculation)
            Row row = table.Rows.Add();
            row.FixedRowHeight = 30; // any height you need for the cell

            // Add a cell with some text
            Cell cell = row.Cells.Add("Click here");
            // Optionally customize the text appearance
            cell.DefaultCellTextState.Font = FontRepository.FindFont("Helvetica");
            cell.DefaultCellTextState.FontSize = 12;
            cell.DefaultCellTextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // ------------------------------------------------------------
            // Calculate the rectangle that covers the cell.
            // Aspose.Pdf.Table does not expose a GetCellPosition method, so we
            // compute the rectangle manually using the known column width and
            // the row height. The origin of a PDF page is the lower‑left corner.
            // ------------------------------------------------------------
            double cellWidth = 200;                     // matches ColumnWidths
            double cellHeight = row.FixedRowHeight;     // 30
            double llx = 0;                             // left‑most X (no left margin)
            double urx = llx + cellWidth;               // right‑most X
            // Y coordinates: page height minus the distance from the top of the page
            double pageHeight = page.PageInfo.Height;
            double ury = pageHeight;                    // top of the page (default placement)
            double lly = ury - cellHeight;              // lower‑left Y of the cell
            Rectangle cellRect = new Rectangle(llx, lly, urx, ury);

            // Create a link annotation that covers the cell's rectangle
            LinkAnnotation link = new LinkAnnotation(page, cellRect)
            {
                // Use an action to open an external URL
                Action = new GoToURIAction("https://example.com"),
                // Visual appearance of the link (optional)
                Color = Aspose.Pdf.Color.Blue
            };
            // Make the link border invisible – Border constructor requires the parent annotation
            link.Border = new Border(link) { Width = 0 };

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the PDF to disk
            doc.Save("HyperlinkedTableCell.pdf");
        }

        Console.WriteLine("PDF with hyperlinked table cell created successfully.");
    }
}
