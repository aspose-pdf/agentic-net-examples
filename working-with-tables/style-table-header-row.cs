using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "styled_table.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a table with two columns
            Table table = new Table
            {
                // Auto‑fit columns to the content
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent,
                // Optional: set column widths if you want fixed sizes
                // ColumnWidths = "150 150"
            };

            // ----- Header row -----
            Row headerRow = table.Rows.Add();
            // First header cell
            Cell th1 = headerRow.Cells.Add("Product");
            th1.BackgroundColor = Color.LightGray;
            th1.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Bold"),
                FontSize = 12,
                FontStyle = FontStyles.Bold,
                ForegroundColor = Color.Black
            };
            // Second header cell
            Cell th2 = headerRow.Cells.Add("Price");
            th2.BackgroundColor = Color.LightGray;
            th2.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Bold"),
                FontSize = 12,
                FontStyle = FontStyles.Bold,
                ForegroundColor = Color.Black
            };

            // ----- Body rows -----
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Widget A");
            dataRow.Cells.Add("$50.00");

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
