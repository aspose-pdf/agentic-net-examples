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

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Create a table with three columns
            Table table = new Table
            {
                // Optional: set explicit column widths (in points)
                ColumnWidths = "100 150 200"
            };

            // Add a single row with three cells
            Row row = table.Rows.Add();
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");
            row.Cells.Add("Cell 3");

            // Insert the table into the first page
            Page page = doc.Pages[1];
            page.Paragraphs.Add(table);

            // After layout, obtain the calculated width of the rendered table
            double renderedWidth = table.GetWidth();
            Console.WriteLine($"Rendered table width: {renderedWidth}");

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}