using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a new table
            Table table = new Table();

            // NOTE: Table does not have a Position property.
            // Position is set via the Left and Top properties (coordinates in points).
            // Set the desired X (left) and Y (top) coordinates.
            float x = 100f; // X coordinate from the left edge
            float y = 500f; // Y coordinate from the bottom edge
            table.Left = x;
            table.Top  = y;

            // Optional: define column widths (comma‑separated or space‑separated string)
            table.ColumnWidths = "100 100 100";

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Cell 1");
            data.Cells.Add("Cell 2");
            data.Cells.Add("Cell 3");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted and saved to '{outputPath}'.");
    }
}