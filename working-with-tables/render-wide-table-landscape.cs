using System;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for text-related helpers if needed

class Program
{
    static void Main()
    {
        const string outputPath = "landscape_table.pdf";

        // Document lifecycle must be wrapped in a using block (see document-disposal-with-using rule)
        using (Document doc = new Document())
        {
            // Add a new page (Pages collection is 1‑based – see page-indexing-one-based rule)
            Page page = doc.Pages.Add();

            // Change orientation to landscape.
            // Setting IsLandscape informs the renderer; also swap width/height for A4.
            page.PageInfo.IsLandscape = true;
            page.SetPageSize(842, 595); // A4 landscape in points (width > height)

            // Create a wide table that will fit the landscape page.
            Table table = new Table
            {
                // Auto‑fit columns to the available width.
                ColumnAdjustment = ColumnAdjustment.AutoFitToWindow,

                // Optional visual styling.
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Define column widths (5 columns, each 150 points wide → total 750 points, fits landscape page).
            table.ColumnWidths = "150 150 150 150 150";

            // Header row.
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");
            header.Cells.Add("Header 4");
            header.Cells.Add("Header 5");

            // Sample data rows.
            for (int i = 0; i < 10; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"R{i + 1}C1");
                row.Cells.Add($"R{i + 1}C2");
                row.Cells.Add($"R{i + 1}C3");
                row.Cells.Add($"R{i + 1}C4");
                row.Cells.Add($"R{i + 1}C5");
            }

            // Add the table to the page's paragraph collection.
            page.Paragraphs.Add(table);

            // Save the PDF (document.Save(string) always writes PDF – see save-to-non-pdf-always-use-save-options rule).
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
