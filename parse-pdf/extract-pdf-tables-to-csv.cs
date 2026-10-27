using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "TablesCsv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // TableAbsorber extracts tables from the document pages
            TableAbsorber tableAbsorber = new TableAbsorber();

            // Visit each page – the newer API uses Visit(page) instead of Page.Accept(...)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                tableAbsorber.Visit(doc.Pages[pageIndex]);
            }

            int tableIndex = 1;

            // Iterate over each extracted table
            foreach (var table in tableAbsorber.TableList)
            {
                string csvPath = Path.Combine(outputDir, $"Table_{tableIndex}.csv");

                using (StreamWriter writer = new StreamWriter(csvPath))
                {
                    // RowList and CellList are the correct collections
                    foreach (var row in table.RowList)
                    {
                        List<string> csvCells = new List<string>();

                        foreach (var cell in row.CellList)
                        {
                            // Concatenate all text fragments inside the cell
                            string cellText = string.Empty;
                            foreach (var fragment in cell.TextFragments)
                            {
                                cellText += fragment.Text;
                            }
                            // Escape double quotes and wrap the cell text in quotes
                            cellText = cellText?.Replace("\"", "\"\"") ?? string.Empty;
                            csvCells.Add($"\"{cellText}\"");
                        }

                        writer.WriteLine(string.Join(",", csvCells));
                    }
                }

                Console.WriteLine($"Table {tableIndex} saved to '{csvPath}'.");
                tableIndex++;
            }

            if (tableIndex == 1)
            {
                Console.WriteLine("No tables were found in the PDF.");
            }
        }
    }
}
