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

            // Create a table with three columns of width 100 each
            Table table = new Table
            {
                ColumnWidths = "100 100 100",
                // Apply a default border of width 1 to all sides of each cell
                DefaultCellBorder = new BorderInfo(BorderSide.All, 1)
            };

            // Example header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Example data row
            Row data = table.Rows.Add();
            data.Cells.Add("Cell 1");
            data.Cells.Add("Cell 2");
            data.Cells.Add("Cell 3");

            // Add the table to the page's content
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and saved to '{outputPath}'.");
    }
}