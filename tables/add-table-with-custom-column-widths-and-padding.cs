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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a new table with column widths and default cell padding (4 points on all sides)
            Table table = new Table
            {
                ColumnWidths = "80 160",
                // MarginInfo constructor requires left, bottom, right, top values
                DefaultCellPadding = new MarginInfo(4, 4, 4, 4)
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");

            // Add a data row (example content)
            Row data = table.Rows.Add();
            data.Cells.Add("Value A");
            data.Cells.Add("Value B");

            // Insert the table into the first page of the document (1‑based indexing)
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and PDF saved to '{outputPath}'.");
    }
}
