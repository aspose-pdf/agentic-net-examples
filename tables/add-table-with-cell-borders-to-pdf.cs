using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the existing PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (1‑based indexing).
            Page page = doc.Pages[1];

            // Create a table and configure its default cell border.
            Table table = new Table
            {
                // Example column widths; adjust as needed.
                ColumnWidths = "150 150 150"
            };

            // Set a border of width 1 on all sides of each cell.
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 1);

            // Add a simple header row.
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add a data row.
            Row data = table.Rows.Add();
            data.Cells.Add("Cell A1");
            data.Cells.Add("Cell A2");
            data.Cells.Add("Cell A3");

            // Insert the table into the page's paragraph collection.
            page.Paragraphs.Add(table);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and saved to '{outputPath}'.");
    }
}
