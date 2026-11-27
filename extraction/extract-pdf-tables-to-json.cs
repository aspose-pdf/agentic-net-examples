using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "TablesJson";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Use TableAbsorber (the core API) to extract tables from the page
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(page);

                // Process each detected table
                for (int t = 0; t < absorber.TableList.Count; t++)
                {
                    var absorbedTable = absorber.TableList[t];
                    var rows = new List<List<string>>();

                    // Build a nested list of rows → cells
                    foreach (var absorbedRow in absorbedTable.RowList)
                    {
                        var cells = new List<string>();
                        foreach (var absorbedCell in absorbedRow.CellList)
                        {
                            // Concatenate all text fragments inside the cell
                            string cellText = string.Join(" ",
                                absorbedCell.TextFragments.Select(tf => tf.Text ?? string.Empty));
                            cells.Add(cellText);
                        }
                        rows.Add(cells);
                    }

                    // Serialize the table structure to formatted JSON
                    string json = JsonSerializer.Serialize(
                        rows,
                        new JsonSerializerOptions { WriteIndented = true });

                    // Create a unique file name per table
                    string fileName = Path.Combine(
                        outputDir,
                        $"Table_page{pageNum}_index{t + 1}.json");

                    // Write JSON to disk
                    File.WriteAllText(fileName, json);
                }
            }
        }

        Console.WriteLine("Table extraction completed.");
    }
}
