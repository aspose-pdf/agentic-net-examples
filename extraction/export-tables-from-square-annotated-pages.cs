using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string csvPath   = "AnnotatedPagesTables.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load PDF and open CSV writer; both are wrapped in using for deterministic disposal
            using (Document doc = new Document(inputPath))
            using (StreamWriter csvWriter = new StreamWriter(csvPath, false))
            {
                // CSV header
                csvWriter.WriteLine("PageNumber,TableIndex,RowIndex,ColumnIndex,CellText");

                // Pages are 1‑based (Aspose.Pdf rule)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Determine if the page contains at least one SquareAnnotation
                    bool hasSquare = page.Annotations.Any(a => a is SquareAnnotation);
                    if (!hasSquare)
                        continue; // Skip pages without SquareAnnotation

                    // Extract tables from the page using TableAbsorber (core API)
                    TableAbsorber absorber = new TableAbsorber();
                    absorber.Visit(page); // <-- correct method, not Extract()

                    int tableIdx = 0;
                    foreach (var absorbedTable in absorber.TableList)
                    {
                        tableIdx++;
                        int rowIdx = 0;
                        foreach (var absorbedRow in absorbedTable.RowList)
                        {
                            rowIdx++;
                            int colIdx = 0;
                            foreach (var absorbedCell in absorbedRow.CellList)
                            {
                                colIdx++;
                                // Concatenate all text fragments inside the cell
                                string cellText = string.Concat(absorbedCell.TextFragments.Select(tf => tf.Text ?? string.Empty));
                                // Escape double quotes for CSV compliance
                                cellText = cellText.Replace("\"", "\"\"");
                                csvWriter.WriteLine($"{i},{tableIdx},{rowIdx},{colIdx},\"{cellText}\"");
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Tables exported to '{csvPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
