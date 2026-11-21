using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // for BorderInfo and BorderSide

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a table and configure its columns
            Table table = new Table
            {
                // Two columns, each 100 points wide
                ColumnWidths = "100 100"
            };

            // Add a row with two cells
            Row row = table.Rows.Add();
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");

            // Configure the table border: width = 3 points on all sides
            table.Border = new BorderInfo(BorderSide.All, 3);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and saved to '{outputPath}'.");
    }
}