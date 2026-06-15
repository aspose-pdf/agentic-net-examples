using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextFragment if needed

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Create a new Table instance
            Table table = new Table();

            // Position the table on the page
            table.Left = 100;   // X coordinate
            table.Top  = 500;   // Y coordinate

            // Set ZIndex to control overlapping order (higher value = on top)
            table.ZIndex = 10;

            // Define column widths (optional)
            table.ColumnWidths = "100 150";

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Cell A");
            data.Cells.Add("Cell B");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF (using the standard Save method)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with table ZIndex to '{outputPath}'.");
    }
}