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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to add the table to
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create a table with the required settings
            Table table = new Table
            {
                ColumnAdjustment = ColumnAdjustment.AutoFitToWindow, // Auto‑fit columns to page width
                // MarginInfo constructor requires left, bottom, right, top values
                DefaultCellPadding = new MarginInfo(2, 2, 2, 2) // 2‑point padding inside each cell
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Value A");
            data.Cells.Add("Value B");

            // Insert the table into the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
