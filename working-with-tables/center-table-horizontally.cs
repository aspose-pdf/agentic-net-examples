using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "centered_table.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page (Pages.Add returns the newly created page)
            Page page = doc.Pages.Add();

            // Define the table rectangle (full page width, positioned near the top)
            double llx = 0;
            double lly = page.PageInfo.Height - 100; // lower‑left Y (from bottom)
            double urx = page.PageInfo.Width;
            double ury = page.PageInfo.Height - 20;  // upper‑right Y (from bottom)

            // Create the table (parameterless constructor)
            Table table = new Table();

            // Position the table using Left/Top properties (Aspose.Pdf.Table does not have SetPosition)
            table.Left = (float)llx;
            table.Top = (float)lly;

            // Align the table horizontally to the center of the page
            table.HorizontalAlignment = HorizontalAlignment.Center;

            // Define column widths (two equal columns covering the table width)
            double columnWidth = (urx - llx) / 2;
            // ColumnWidths expects a space‑separated string, not a double array
            table.ColumnWidths = $"{columnWidth} {columnWidth}";

            // Optional: auto‑fit columns to content (helps when text is longer than the preset width)
            table.ColumnAdjustment = ColumnAdjustment.AutoFitToContent;

            // Add a sample row with two cells
            Row row = table.Rows.Add();
            Cell cell1 = row.Cells.Add("Header 1");
            Cell cell2 = row.Cells.Add("Header 2");

            // Optional styling (using Aspose.Pdf.Color to avoid System.Drawing)
            cell1.BackgroundColor = Aspose.Pdf.Color.LightGray;
            cell2.BackgroundColor = Aspose.Pdf.Color.LightGray;

            // Insert the table into the page's content
            page.Paragraphs.Add(table);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with centered table saved to '{outputPath}'.");
    }
}
