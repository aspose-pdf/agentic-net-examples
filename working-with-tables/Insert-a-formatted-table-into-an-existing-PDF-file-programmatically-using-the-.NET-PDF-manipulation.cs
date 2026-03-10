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

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Create a new table and configure its appearance
            Table table = new Table
            {
                // NOTE: ColumnAdjustment.AutoFit is not available in the current Aspose.Pdf version,
                // so we rely on the default behavior (auto‑fit) and omit this setting.
                // Border around the whole table
                Border = new BorderInfo(BorderSide.All, 1, Aspose.Pdf.Color.DarkGray),
                // Background color for the table area
                BackgroundColor = Aspose.Pdf.Color.LightGray,
                // Default cell border and padding
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
                // Define column widths (points)
                ColumnWidths = "200 200"
            };

            // ----- Header row -----
            Row header = table.Rows.Add();
            Cell headerCell1 = header.Cells.Add();
            headerCell1.Paragraphs.Add(new TextFragment("Header 1"));
            Cell headerCell2 = header.Cells.Add();
            headerCell2.Paragraphs.Add(new TextFragment("Header 2"));

            // ----- Data row -----
            Row dataRow = table.Rows.Add();
            Cell dataCell1 = dataRow.Cells.Add();
            dataCell1.Paragraphs.Add(new TextFragment("Cell 1"));
            Cell dataCell2 = dataRow.Cells.Add();
            dataCell2.Paragraphs.Add(new TextFragment("Cell 2"));

            // Position the table on the page (coordinates are from the bottom‑left corner)
            table.Left = 100; // distance from left edge
            table.Top = 500;  // distance from bottom edge

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted and saved to '{outputPath}'.");
    }
}
