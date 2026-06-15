using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document inside a using block for proper disposal.
        using (Document doc = new Document(inputPath))
        {
            // Create a new table and configure basic properties.
            Table table = new Table
            {
                // Example: set column widths (two columns, each 200 points wide).
                ColumnWidths = "200 200",
                // Optional: set table border.
                Border = new BorderInfo(BorderSide.All, 1.0f, Aspose.Pdf.Color.Black)
            };

            // First row (default height).
            Row firstRow = table.Rows.Add();
            firstRow.Cells.Add("First Row, Cell 1");
            firstRow.Cells.Add("First Row, Cell 2");

            // Second row – set a custom fixed height.
            Row secondRow = table.Rows.Add();
            secondRow.Cells.Add("Second Row, Cell 1");
            secondRow.Cells.Add("Second Row, Cell 2");
            // Assign a fixed row height (e.g., 50 points).
            secondRow.FixedRowHeight = 50.0;

            // Add the table to the first page of the document.
            Page page = doc.Pages[1];
            page.Paragraphs.Add(table);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom row height to '{outputPath}'.");
    }
}