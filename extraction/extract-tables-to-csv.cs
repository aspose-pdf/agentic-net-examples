using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            int pageCount = pdfDoc.Pages.Count;
            int tableIndexOverall = 1;

            // Iterate through each page and extract tables using TableAbsorber (core API)
            for (int pageNum = 1; pageNum <= pageCount; pageNum++)
            {
                TableAbsorber absorber = new TableAbsorber();
                // The core API uses Visit, not Accept
                absorber.Visit(pdfDoc.Pages[pageNum]);

                // Each AbsorbedTable represents a detected table on the page
                foreach (var table in absorber.TableList)
                {
                    // Build a distinct file name for each table
                    string csvPath = $"Table_Page{pageNum}_{tableIndexOverall}.csv";

                    // Write the table content to CSV
                    WriteTableToCsv(table, csvPath);

                    Console.WriteLine($"Exported table {tableIndexOverall} to '{csvPath}'.");
                    tableIndexOverall++;
                }
            }
        }

        Console.WriteLine("CSV export of tables completed.");
    }

    /// <summary>
    /// Writes an AbsorbedTable (result of TableAbsorber) to a CSV file using simple comma‑separated values.
    /// Cells are escaped according to RFC 4180 (quotes doubled, field wrapped in quotes if needed).
    /// </summary>
    private static void WriteTableToCsv(AbsorbedTable table, string filePath)
    {
        var sb = new StringBuilder();
        foreach (var row in table.RowList)
        {
            var cellValues = new string[row.CellList.Count];
            for (int i = 0; i < row.CellList.Count; i++)
            {
                var cell = row.CellList[i];
                // Concatenate all text fragments inside the cell
                var cellText = new StringBuilder();
                foreach (var fragment in cell.TextFragments)
                {
                    cellText.Append(fragment.Text);
                }
                cellValues[i] = EscapeForCsv(cellText.ToString());
            }
            sb.AppendLine(string.Join(",", cellValues));
        }
        File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
    }

    /// <summary>
    /// Escapes a string for CSV output. If the value contains a quote, comma, or line‑break,
    /// it is wrapped in double quotes and internal quotes are doubled.
    /// </summary>
    private static string EscapeForCsv(string value)
    {
        if (value == null)
            return string.Empty;

        bool mustQuote = value.Contains(",") || value.Contains("\"") || value.Contains("\n") || value.Contains("\r");
        if (mustQuote)
        {
            var escaped = value.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        return value;
    }
}
