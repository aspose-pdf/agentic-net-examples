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
        const string outputDir = "Tables";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            int tableCount = 0;

            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // TableAbsorber extracts tables from a page
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(page);

                // Iterate over each detected table
                foreach (var table in absorber.TableList) // AbsorbedTable
                {
                    tableCount++;
                    string csvPath = Path.Combine(outputDir, $"Table{tableCount}.csv");

                    using (StreamWriter writer = new StreamWriter(csvPath))
                    {
                        foreach (var row in table.RowList) // AbsorbedRow
                        {
                            List<string> cellTexts = new List<string>();

                            foreach (var cell in row.CellList) // AbsorbedCell
                            {
                                // Concatenate all text fragments inside the cell
                                string cellText = string.Empty;
                                foreach (var fragment in cell.TextFragments) // TextFragment
                                {
                                    cellText += fragment.Text;
                                }

                                // Escape commas, quotes, and newlines per CSV rules
                                string escaped = cellText?.Replace("\"", "\"\"") ?? string.Empty;
                                if (escaped.Contains(",") || escaped.Contains("\"") || escaped.Contains("\n"))
                                    escaped = $"\"{escaped}\"";

                                cellTexts.Add(escaped);
                            }

                            writer.WriteLine(string.Join(",", cellTexts));
                        }
                    }

                    Console.WriteLine($"Saved {csvPath}");
                }
            }

            if (tableCount == 0)
                Console.WriteLine("No tables found in the document.");
        }
    }
}
