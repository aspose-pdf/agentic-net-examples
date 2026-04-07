using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // <-- added for TextFragment

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "adjusted_columns.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Create a new table and add it to the first page
            Table table = new Table
            {
                // Adjust columns to fit the available width of the page
                ColumnAdjustment = ColumnAdjustment.AutoFitToWindow
            };

            // Example: original column widths (in points)
            double[] originalWidths = { 120.0, 80.0, 200.0, 100.0 };

            // Calculate total width
            double total = originalWidths.Sum();

            // Convert each width to a percentage of the total
            // Aspose.Pdf expects percentages followed by the % sign, separated by blanks
            string[] percentStrings = originalWidths
                .Select(w => $"{(w / total * 100):F2}%")
                .ToArray();

            // Assign the calculated percentages to the table
            table.ColumnWidths = string.Join(" ", percentStrings);

            // Add a simple row with cells to visualize the columns
            Row row = table.Rows.Add();
            foreach (var width in originalWidths)
            {
                Cell cell = row.Cells.Add();
                cell.Paragraphs.Add(new TextFragment($"Width: {width}"));
            }

            // Add the table to the first page of the document
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with adjusted column widths: {outputPath}");
    }
}
