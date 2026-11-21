using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // TableAbsorber extracts tables from the document
            TableAbsorber absorber = new TableAbsorber();

            // Visit the whole document (all pages) to populate TableList
            absorber.Visit(doc);

            int tableIndex = 1;

            // Iterate over each detected table
            foreach (AbsorbedTable table in absorber.TableList)
            {
                string csvPath = $"Table_{tableIndex}.csv";

                // Write the table to a CSV file
                using (StreamWriter writer = new StreamWriter(csvPath, false, Encoding.UTF8))
                {
                    foreach (AbsorbedRow row in table.RowList)
                    {
                        List<string> cellValues = new List<string>();

                        foreach (AbsorbedCell cell in row.CellList)
                        {
                            // Concatenate all text fragments inside the cell
                            string cellText = string.Concat(cell.TextFragments.Select(tf => tf.Text));
                            cellValues.Add(EscapeCsv(cellText));
                        }

                        // Join cells with commas and write the line
                        writer.WriteLine(string.Join(",", cellValues));
                    }
                }

                Console.WriteLine($"Extracted Table {tableIndex} to '{csvPath}'.");
                tableIndex++;
            }

            if (tableIndex == 1)
            {
                Console.WriteLine("No tables were found in the PDF.");
            }
        }
    }

    // Helper method to escape CSV fields according to RFC 4180
    static string EscapeCsv(string field)
    {
        if (field == null)
            return string.Empty;

        bool mustQuote = field.Contains(",") || field.Contains("\"") || field.Contains("\r") || field.Contains("\n");
        if (mustQuote)
        {
            // Escape double quotes by doubling them
            string escaped = field.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        else
        {
            return field;
        }
    }
}