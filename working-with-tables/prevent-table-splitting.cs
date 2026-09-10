using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for BorderInfo and BorderSide if needed

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

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a new table
            Table table = new Table
            {
                // Disable automatic splitting across pages
                IsBroken = false,

                // Example column widths (adjust as needed)
                ColumnWidths = "150 150 150",

                // Optional: set a simple border for visibility
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
            };

            // Add a single row with three cells
            Row row = table.Rows.Add();
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");
            row.Cells.Add("Cell 3");

            // Add the table to the first page of the document
            Page page = doc.Pages[1]; // 1‑based indexing
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with table that will not split: '{outputPath}'.");
    }
}