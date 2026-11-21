using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (var doc = new Document(inputPath))
        {
            // Create a table with three columns of equal width and solid cell borders
            var table = new Table
            {
                // Equal column widths (points). Adjust as needed.
                ColumnWidths = "33 33 33",
                // Apply a solid 1‑point border to every cell
                DefaultCellBorder = new BorderInfo(BorderSide.All, 1f)
            };

            // Header row
            var header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Data row
            var data = table.Rows.Add();
            data.Cells.Add("Cell A1");
            data.Cells.Add("Cell A2");
            data.Cells.Add("Cell A3");

            // Add the table to the first page's paragraph collection
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and PDF saved to '{outputPath}'.");
    }
}
