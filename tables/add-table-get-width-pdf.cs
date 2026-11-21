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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a simple table with two columns
            Table table = new Table();

            // Define column widths (optional)
            table.ColumnWidths = "200 200";

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Column 1");
            header.Cells.Add("Column 2");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Value A");
            data.Cells.Add("Value B");

            // Insert the table into the first page's paragraph collection
            // Pages are 1‑based, so Pages[1] is the first page
            doc.Pages[1].Paragraphs.Add(table);

            // Retrieve the table's width
            double tableWidth = table.GetWidth();
            Console.WriteLine($"Table width: {tableWidth}");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}