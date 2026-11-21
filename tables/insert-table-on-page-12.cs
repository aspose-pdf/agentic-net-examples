using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 12 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 12)
            {
                Console.Error.WriteLine("The document does not contain a page 12.");
                return;
            }

            // Create a new table
            Table table = new Table();

            // Example column widths (adjust as needed)
            table.ColumnWidths = "120 120 120";

            // Apply a default border to all cells
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f);

            // Add a few rows with sample data
            for (int r = 1; r <= 5; r++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Row {r} - Col 1");
                row.Cells.Add($"Row {r} - Col 2");
                row.Cells.Add($"Row {r} - Col 3");
            }

            // Replace the existing content on page 12 with the new table
            Page page12 = doc.Pages[12];
            page12.Paragraphs.Clear();      // Remove any existing paragraphs/tables
            page12.Paragraphs.Add(table);   // Insert the new table

            // Save the modified PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}