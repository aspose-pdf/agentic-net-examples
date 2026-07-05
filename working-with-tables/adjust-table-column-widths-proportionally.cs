using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "adjusted_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Original column widths (points, can be any unit)
        double[] originalWidths = { 120, 80, 200 };

        // Calculate total width
        double totalWidth = originalWidths.Sum();

        // Build a percentage string like "30.00% 20.00% 50.00%"
        string percentageWidths = string.Join(" ",
            originalWidths.Select(w => $"{(w / totalWidth * 100):F2}%"));

        using (Document doc = new Document(inputPath))
        {
            // Create a table and set proportional column widths
            Table table = new Table
            {
                ColumnAdjustment = ColumnAdjustment.AutoFitToWindow,
                ColumnWidths    = percentageWidths
            };

            // Add a single row with sample cells
            Row row = table.Rows.Add();
            foreach (var _ in originalWidths)
            {
                row.Cells.Add("Sample");
            }

            // Insert the table into the first page
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Proportional table saved to '{outputPath}'.");
    }
}