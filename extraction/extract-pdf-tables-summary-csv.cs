using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string csvPath   = "tables_summary.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Create CSV file and write header
                using (StreamWriter writer = new StreamWriter(csvPath, false))
                {
                    writer.WriteLine("TableIndex,PageNumber,CellCount");

                    int tableIndex = 0;

                    // Pages are 1‑based in Aspose.Pdf
                    for (int i = 1; i <= doc.Pages.Count; i++)
                    {
                        Page page = doc.Pages[i];

                        // Extract tables from the current page
                        TableAbsorber absorber = new TableAbsorber();
                        absorber.Visit(page);

                        // Iterate over all tables found on this page
                        foreach (var absorbedTable in absorber.TableList)
                        {
                            tableIndex++;

                            // Count cells: sum of cells in each row
                            int cellCount = 0;
                            foreach (var row in absorbedTable.RowList)
                            {
                                cellCount += row.CellList.Count;
                            }

                            writer.WriteLine($"{tableIndex},{i},{cellCount}");
                        }
                    }
                }
            }

            Console.WriteLine($"Table summary CSV saved to '{csvPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
