using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextState and FontRepository

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (or add a new one if needed)
            Page page = doc.Pages[1];

            // Create a table
            Table table = new Table
            {
                // Example column widths; adjust as needed
                ColumnWidths = "100 200 100",
                // Optional: set a default border for all cells
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
            };

            // Fixed height to be applied to every row
            double fixedRowHeight = 20.0;

            // ----- Header row -----
            Row header = table.Rows.Add();
            header.FixedRowHeight = fixedRowHeight;
            header.BackgroundColor = Aspose.Pdf.Color.Gray;
            header.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.White
            };
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // ----- Data rows -----
            for (int i = 0; i < 5; i++)
            {
                Row row = table.Rows.Add();
                row.FixedRowHeight = fixedRowHeight; // enforce the same height
                row.Cells.Add($"Row {i + 1} Col 1");
                row.Cells.Add($"Row {i + 1} Col 2");
                row.Cells.Add($"Row {i + 1} Col 3");
            }

            // Position the table on the page (optional margins)
            table.Margin = new MarginInfo { Top = 20, Left = 20 };

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table with fixed row height saved to '{outputPath}'.");
    }
}