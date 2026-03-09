using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "table_output.pdf";

        // Ensure the Document is disposed properly
        using (Document doc = new Document())
        {
            // Add a new page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a table and configure its appearance
            Table table = new Table
            {
                // Auto‑fit columns to content (default behavior, explicit setting removed for compatibility)
                // Define three columns with specific widths
                ColumnWidths = "100 150 200",
                // Set a thin black border for all cells
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black),
                // Add padding inside each cell
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // ----- Header row -----
            Row header = table.Rows.Add();
            // Header style: bold white text on gray background
            header.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                FontStyle = FontStyles.Bold,
                ForegroundColor = Aspose.Pdf.Color.White
            };
            header.BackgroundColor = Aspose.Pdf.Color.Gray;
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // ----- Data rows -----
            for (int i = 1; i <= 5; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Row {i} Col 1");
                row.Cells.Add($"Row {i} Col 2");
                row.Cells.Add($"Row {i} Col 3");
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }
}
