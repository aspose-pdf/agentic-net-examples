using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPath = "input.pdf";

        // Directory where extracted CSV files will be saved
        const string outputDir = "ExtractedTables";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to find tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document
            absorber.Visit(doc);

            int tableIndex = 1;

            // Iterate over each detected table
            foreach (AbsorbedTable table in absorber.TableList)
            {
                // Build a CSV file name for the current table
                string csvPath = Path.Combine(outputDir, $"Table_{tableIndex}.csv");

                // Write the table content to CSV
                using (StreamWriter writer = new StreamWriter(csvPath))
                {
                    // Iterate over rows in the table
                    foreach (var row in table.RowList)
                    {
                        List<string> cellTexts = new List<string>();

                        // Iterate over cells in the row
                        foreach (var cell in row.CellList)
                        {
                            // Concatenate all text fragments inside the cell
                            string cellText = string.Empty;
                            foreach (TextFragment fragment in cell.TextFragments)
                            {
                                cellText += fragment.Text;
                            }

                            // Escape commas and double quotes according to CSV rules
                            if (cellText.Contains("\"") || cellText.Contains(","))
                            {
                                cellText = $"\"{cellText.Replace("\"", "\"\"")}\"";
                            }

                            cellTexts.Add(cellText);
                        }

                        // Write the CSV line for the current row
                        writer.WriteLine(string.Join(",", cellTexts));
                    }
                }

                Console.WriteLine($"Extracted Table {tableIndex} to '{csvPath}'");
                tableIndex++;
            }
        }
    }
}