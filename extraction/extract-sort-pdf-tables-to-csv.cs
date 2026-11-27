using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string csvPath   = "SortedTables.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // TableAbsorber extracts tables from pages
            TableAbsorber absorber = new TableAbsorber();

            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                absorber.Visit(doc.Pages[i]); // extract tables from page i
            }

            // Convert the absorber's TableList (IList<AbsorbedTable>) to a List for LINQ operations
            List<AbsorbedTable> tables = absorber.TableList.ToList();

            // Sort tables by the page they were found on (use PageNum)
            var sortedTables = tables.OrderBy(t => t.PageNum).ToList();

            // Write the sorted tables to a CSV file
            using (StreamWriter writer = new StreamWriter(csvPath, false))
            {
                // CSV header
                writer.WriteLine("PageNumber,RowCount,ColumnCount,Text");

                foreach (var tbl in sortedTables)
                {
                    // Build a flat representation of the table:
                    // rows are separated by ';' and cells within a row by '|'
                    List<string> rowTexts = new List<string>();
                    foreach (var row in tbl.RowList)
                    {
                        List<string> cellTexts = new List<string>();
                        foreach (var cell in row.CellList)
                        {
                            // Concatenate all text fragments inside the cell
                            string cellText = string.Empty;
                            if (cell.TextFragments != null && cell.TextFragments.Count > 0)
                            {
                                cellText = string.Join(" ", cell.TextFragments.Select(tf => tf.Text));
                            }
                            // Clean cell text to avoid line breaks in CSV and ensure non‑null
                            string clean = (cellText ?? string.Empty)
                                            .Replace("\r", " ")
                                            .Replace("\n", " ")
                                            .Trim();
                            cellTexts.Add(clean);
                        }
                        rowTexts.Add(string.Join("|", cellTexts));
                    }
                    string tableText = string.Join(";", rowTexts);

                    // Escape double quotes for CSV compliance
                    tableText = $"\"{tableText.Replace("\"", "\"\"")}\"";

                    // Determine column count (maximum cells in any row)
                    int columnCount = tbl.RowList.Count > 0 ? tbl.RowList.Max(r => r.CellList.Count) : 0;

                    writer.WriteLine($"{tbl.PageNum},{tbl.RowList.Count},{columnCount},{tableText}");
                }
            }

            Console.WriteLine($"Extracted {sortedTables.Count} tables to '{csvPath}'.");
        }
    }
}
