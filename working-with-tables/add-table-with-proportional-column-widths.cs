using System;
using System.IO;
using System.Linq;
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

        // Define the desired column widths (in points or any unit)
        double[] columnWidths = { 120, 80, 100 };

        // Calculate the total width of all columns
        double totalWidth = columnWidths.Sum();

        // Convert each width to a percentage of the total width
        string[] widthPercentages = columnWidths
            .Select(w => (w / totalWidth * 100).ToString("0.##") + "%")
            .ToArray();

        // Build the ColumnWidths string expected by Aspose.Pdf (e.g., "40% 27% 33%")
        string columnWidthsString = string.Join(" ", widthPercentages);

        // Load the source PDF and add a table with proportional column widths
        using (Document doc = new Document(inputPath))
        {
            // Create a table and set its column widths using percentages
            Table table = new Table
            {
                ColumnWidths = columnWidthsString,
                ColumnAdjustment = ColumnAdjustment.AutoFitToWindow
            };

            // Add a single row with cells (placeholder content)
            Row row = table.Rows.Add();
            foreach (var _ in columnWidths)
            {
                Cell cell = row.Cells.Add();
                cell.Paragraphs.Add(new TextFragment("Cell"));
            }

            // Insert the table into the first page of the document
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Proportional table saved to '{outputPath}'.");
    }
}