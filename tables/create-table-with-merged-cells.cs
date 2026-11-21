using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure we have at least one page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a visual table with three columns
            Table table = new Table
            {
                // Define equal column widths (100 points each)
                ColumnWidths = "100 100 100",
                // Optional visual styling
                Border = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                // Position the table on the page
                Margin = new MarginInfo { Top = 50, Left = 50 }
            };

            // First row – three separate cells
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Cell 1‑1");
            row1.Cells.Add("Cell 1‑2");
            row1.Cells.Add("Cell 1‑3");

            // Second row – three separate cells
            Row row2 = table.Rows.Add();
            row2.Cells.Add("Cell 2‑1");
            row2.Cells.Add("Cell 2‑2");
            row2.Cells.Add("Cell 2‑3");

            // Third row – a single cell that spans all three columns
            Row row3 = table.Rows.Add();
            Cell mergedCell = row3.Cells.Add("Merged across three columns");
            mergedCell.ColSpan = 3; // Merge across three columns

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF (still inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table with merged cells saved to '{outputPath}'.");
    }
}