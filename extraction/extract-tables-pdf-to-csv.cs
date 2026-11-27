using System;
using System.IO;
using System.Linq;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedTables";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];

                // Use TableAbsorber to extract tables from the current page
                var tableAbsorber = new TableAbsorber();
                tableAbsorber.Visit(page);

                // If no tables were found on this page, skip CSV creation
                if (tableAbsorber.TableList.Count == 0)
                    continue;

                // Build CSV content for the current page
                var sb = new StringBuilder();
                int currentRow = 0; // not used for CSV but kept for logic parity

                foreach (var absorbedTable in tableAbsorber.TableList)
                {
                    foreach (var absorbedRow in absorbedTable.RowList)
                    {
                        var cellValues = absorbedRow.CellList
                            .Select(cell => EscapeForCsv(string.Concat(cell.TextFragments.Select(tf => tf.Text))))
                            .ToArray();
                        sb.AppendLine(string.Join(",", cellValues));
                        currentRow++;
                    }
                    // Add an empty line between consecutive tables for readability
                    sb.AppendLine();
                }

                string csvPath = Path.Combine(outputFolder, $"Page_{pageNum}.csv");
                File.WriteAllText(csvPath, sb.ToString(), Encoding.UTF8);
                Console.WriteLine($"Extracted tables from page {pageNum} into CSV file 'Page_{pageNum}.csv'.");
            }
        }

        Console.WriteLine("Table extraction completed.");
    }

    /// <summary>
    /// Escapes a string so it can be safely placed inside a CSV cell.
    /// If the value contains a comma, quote, or newline, it is wrapped in double quotes
    /// and any existing double quotes are escaped by doubling them.
    /// </summary>
    private static string EscapeForCsv(string value)
    {
        if (value == null)
            return string.Empty;

        bool mustQuote = value.Contains(',') || value.Contains('"') || value.Contains('\n') || value.Contains('\r');
        if (mustQuote)
        {
            var escaped = value.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        return value;
    }
}
