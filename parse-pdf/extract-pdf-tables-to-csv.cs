using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "TablesCsv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPdfPath))
        {
            // Create TableAbsorber to find tables (provided API)
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document
            absorber.Visit(doc);

            // Iterate over each detected table
            for (int tableIndex = 0; tableIndex < absorber.TableList.Count; tableIndex++)
            {
                var absorbedTable = absorber.TableList[tableIndex];
                string csvPath = Path.Combine(outputFolder, $"table_{tableIndex + 1}.csv");

                using (StreamWriter writer = new StreamWriter(csvPath))
                {
                    // Iterate rows
                    foreach (var row in absorbedTable.RowList)
                    {
                        List<string> cellValues = new List<string>();

                        // Iterate cells in the current row
                        foreach (var cell in row.CellList)
                        {
                            // Concatenate all text fragments inside the cell
                            string cellText = string.Concat(cell.TextFragments.Select(f => f.Text));

                            // Escape double quotes for CSV compliance
                            string escaped = cellText.Replace("\"", "\"\"");

                            // Enclose each field in double quotes
                            cellValues.Add($"\"{escaped}\"");
                        }

                        // Write CSV line
                        writer.WriteLine(string.Join(",", cellValues));
                    }
                }

                Console.WriteLine($"Table {tableIndex + 1} exported to: {csvPath}");
            }
        }
    }
}