using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "merged_header_table.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // 1) Header table – a single‑column table that acts as a merged header
            // -----------------------------------------------------------------
            Table headerTable = new Table
            {
                ColumnWidths = "300", // full width of the page (adjust as needed)
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            Row headerRow = headerTable.Rows.Add();
            Cell mergedHeader = headerRow.Cells.Add("Merged Header");
            mergedHeader.BackgroundColor = Aspose.Pdf.Color.LightGray;
            mergedHeader.DefaultCellTextState = new TextState
            {
                FontSize = 14,
                Font = FontRepository.FindFont("Helvetica"),
                ForegroundColor = Aspose.Pdf.Color.Blue,
                FontStyle = FontStyles.Bold
            };

            // Add the header table to the page
            page.Paragraphs.Add(headerTable);

            // Add a small vertical space between the two tables
            page.Paragraphs.Add(new TextFragment("\n"));

            // ---------------------------------------------------------------
            // 2) Data table – three equal columns with sub‑header and rows
            // ---------------------------------------------------------------
            Table dataTable = new Table
            {
                ColumnWidths = "100 100 100", // three columns, each 100 units wide
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Sub‑header row with individual column titles
            Row subHeader = dataTable.Rows.Add();
            subHeader.Cells.Add("Column 1");
            subHeader.Cells.Add("Column 2");
            subHeader.Cells.Add("Column 3");

            // Add a few data rows
            for (int i = 1; i <= 5; i++)
            {
                Row dataRow = dataTable.Rows.Add();
                dataRow.Cells.Add($"Row {i} - A");
                dataRow.Cells.Add($"Row {i} - B");
                dataRow.Cells.Add($"Row {i} - C");
            }

            // Add the data table to the page's paragraph collection
            page.Paragraphs.Add(dataTable);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created: {outputPath}");
    }
}
