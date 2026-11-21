using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (add a new page if needed)
            Page page = doc.Pages[1];

            // Create a table and set its broken behavior to keep rows together on the same page
            Table table = new Table
            {
                Broken = TableBroken.VerticalInSamePage,
                // Example column widths (three equal columns)
                ColumnWidths = "100 100 100"
            };

            // Header row (optional)
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");
            foreach (Cell cell in header.Cells)
            {
                cell.DefaultCellTextState = new TextState
                {
                    FontSize = 12,
                    FontStyle = FontStyles.Bold
                };
            }

            // Data rows
            for (int i = 1; i <= 5; i++)
            {
                Row dataRow = table.Rows.Add();
                dataRow.Cells.Add($"Item {i}");
                dataRow.Cells.Add($"Value {i * 10}");
                dataRow.Cells.Add($"Desc {i}");
            }

            // Footer row
            Row footer = table.Rows.Add();
            Cell footerCell = footer.Cells.Add("Footer text");
            footerCell.ColSpan = 3; // span all columns
            footerCell.DefaultCellTextState = new TextState
            {
                FontSize = 10,
                FontStyle = FontStyles.Italic,
                ForegroundColor = Aspose.Pdf.Color.Gray
            };
            footerCell.BackgroundColor = Aspose.Pdf.Color.LightGray;

            // Position the table on the page
            page.Paragraphs.Add(table);
            table.Margin = new MarginInfo(50, 0, 0, 0); // left margin
            table.VerticalAlignment = VerticalAlignment.Top;
            table.Top = 700; // Y coordinate from bottom

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}