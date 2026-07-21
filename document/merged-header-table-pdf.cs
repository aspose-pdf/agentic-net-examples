using System;
using System.IO;
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

            // -------------------------------------------------
            // Header table – a single cell that visually spans the three data columns
            // -------------------------------------------------
            Table headerTable = new Table
            {
                // The total width of the three data columns (3 * 100)
                ColumnWidths = "300",
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Black),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            Row headerRow = headerTable.Rows.Add();
            Cell mergedHeader = headerRow.Cells.Add("Merged Header");
            mergedHeader.BackgroundColor = Color.DarkBlue;
            mergedHeader.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                FontStyle = FontStyles.Bold,
                ForegroundColor = Color.White
            };

            // Add the header table to the page
            page.Paragraphs.Add(headerTable);

            // -------------------------------------------------
            // Data table – three columns of equal width
            // -------------------------------------------------
            Table dataTable = new Table
            {
                ColumnWidths = "100 100 100",
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Black),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Header row for the data table (no merged cells here)
            Row dataHeader = dataTable.Rows.Add();
            Cell col1Header = dataHeader.Cells.Add("First Header");
            col1Header.BackgroundColor = Color.DarkBlue;
            col1Header.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                FontStyle = FontStyles.Bold,
                ForegroundColor = Color.White
            };
            Cell col2Header = dataHeader.Cells.Add("Second Header");
            col2Header.BackgroundColor = Color.DarkBlue;
            col2Header.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                FontStyle = FontStyles.Bold,
                ForegroundColor = Color.White
            };
            Cell col3Header = dataHeader.Cells.Add("Third Header");
            col3Header.BackgroundColor = Color.DarkBlue;
            col3Header.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                FontStyle = FontStyles.Bold,
                ForegroundColor = Color.White
            };

            // Add some data rows
            for (int i = 0; i < 5; i++)
            {
                Row dataRow = dataTable.Rows.Add();
                dataRow.Cells.Add($"Row {i + 1} - Col 1");
                dataRow.Cells.Add($"Row {i + 1} - Col 2");
                dataRow.Cells.Add($"Row {i + 1} - Col 3");
            }

            // Add the data table below the header table
            page.Paragraphs.Add(dataTable);

            // Save the PDF to disk
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created successfully at '{outputPath}'.");
    }
}
