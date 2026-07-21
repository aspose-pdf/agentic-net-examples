using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "adjusted_columns.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Assume we want to adjust the first table on the first page
            Page firstPage = doc.Pages[1];
            Table table = firstPage.Paragraphs
                                 .OfType<Table>()
                                 .FirstOrDefault();

            if (table == null)
            {
                Console.WriteLine("No table found on the first page.");
                doc.Save(outputPath);
                return;
            }

            // Get the current column widths string (e.g., "120 150 200")
            string widthsStr = table.ColumnWidths;
            if (string.IsNullOrWhiteSpace(widthsStr))
            {
                Console.WriteLine("Table has no explicit column widths.");
                doc.Save(outputPath);
                return;
            }

            // Parse the widths into numeric values (points).
            double[] widths = widthsStr
                .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => double.Parse(s, CultureInfo.InvariantCulture))
                .ToArray();

            double total = widths.Sum();
            if (total == 0)
            {
                Console.WriteLine("Total column width is zero; cannot adjust.");
                doc.Save(outputPath);
                return;
            }

            // Compute each width as a percentage of the total
            string[] percentWidths = widths
                .Select(w => $"{(w / total * 100).ToString("0.##", CultureInfo.InvariantCulture)}%")
                .ToArray();

            // Assign the new percentage‑based widths back to the table
            table.ColumnWidths = string.Join(" ", percentWidths);

            // Optionally set the column adjustment mode to auto‑fit to window
            table.ColumnAdjustment = ColumnAdjustment.AutoFitToWindow;

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Column widths adjusted and saved to '{outputPath}'.");
    }
}
