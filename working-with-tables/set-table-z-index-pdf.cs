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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a new table
            Table table = new Table();

            // Position the table on the page
            table.Left = 100;   // X coordinate
            table.Top = 500;    // Y coordinate

            // Define column widths (two columns in this example)
            table.ColumnWidths = "120 180";

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Column A");
            header.Cells.Add("Column B");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Value 1");
            data.Cells.Add("Value 2");

            // Set Z‑index: larger values are drawn over smaller values
            table.ZIndex = 10;

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with table ZIndex to '{outputPath}'.");
    }
}