using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a table that will be added to the page
            Table table = new Table();

            // ----- Custom styling for the table -----
            // Center the table on the page
            table.Alignment = HorizontalAlignment.Center;

            // Light gray background for the whole table
            table.BackgroundColor = Aspose.Pdf.Color.LightGray;

            // Table border (black, 1 point) – use BorderInfo for tables
            table.Border = new BorderInfo(BorderSide.All, 1);

            // Default cell border (dark gray, 0.5 point) – use BorderInfo for cells
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f);

            // Default cell padding (5 points on each side)
            table.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);

            // Default text style for all cells (Helvetica, 12 pt, blue)
            table.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.Blue
            };
            // -----------------------------------------

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Product");
            header.Cells.Add("Price");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Widget");
            data.Cells.Add("$10");

            // Insert the styled table into the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the resulting PDF (PDF format does not require explicit SaveOptions)
            doc.Save("styled_table.pdf");
        }

        Console.WriteLine("PDF with styled table saved as 'styled_table.pdf'.");
    }
}
