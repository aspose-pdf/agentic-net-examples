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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Access the first page (Aspose.Pdf uses 1‑based indexing)
                Page page = doc.Pages[1];

                // Create a table and configure its appearance
                Table table = new Table
                {
                    // Define three equal columns
                    ColumnWidths = "100 100 100",
                    // Add a thin border around each cell
                    DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                    // Set ZIndex to control overlapping order (higher value = front)
                    ZIndex = 10
                };

                // Add a single row with three cells
                Row row = table.Rows.Add();
                row.Cells.Add("Cell 1");
                row.Cells.Add("Cell 2");
                row.Cells.Add("Cell 3");

                // Place the table on the page
                page.Paragraphs.Add(table);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Table with ZIndex saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}