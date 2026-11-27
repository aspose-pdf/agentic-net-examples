using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string csvPath = "tables_report.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Extract tables from the whole document.
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc);

            // Map page number (1‑based) to the tables found on that page.
            var pageTableMap = new Dictionary<int, List<AbsorbedTable>>();
            foreach (AbsorbedTable table in absorber.TableList)
            {
                // Correct property name – AbsorbedTable exposes PageNum, not PageNumber.
                int pageNumber = table.PageNum;
                if (!pageTableMap.ContainsKey(pageNumber))
                    pageTableMap[pageNumber] = new List<AbsorbedTable>();
                pageTableMap[pageNumber].Add(table);
            }

            // Write CSV header and rows.
            using (StreamWriter writer = new StreamWriter(csvPath))
            {
                writer.WriteLine("PageNumber,TableIndex,CellCount");

                // Ensure deterministic order by sorting page numbers.
                foreach (var kvp in pageTableMap.OrderBy(p => p.Key))
                {
                    int pageNumber = kvp.Key;
                    List<AbsorbedTable> tablesOnPage = kvp.Value;

                    for (int i = 0; i < tablesOnPage.Count; i++)
                    {
                        AbsorbedTable table = tablesOnPage[i];
                        int cellCount = 0;
                        foreach (var row in table.RowList)
                        {
                            cellCount += row.CellList.Count;
                        }
                        writer.WriteLine($"{pageNumber},{i + 1},{cellCount}");
                    }
                }
            }

            Console.WriteLine($"CSV report generated at '{csvPath}'.");
        }
    }
}
