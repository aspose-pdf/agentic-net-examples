using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string jsonOutput = "tables.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Extract tables from all pages using TableAbsorber
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc); // populate absorber.TableList

            // Build JSON‑friendly structure: List of tables -> rows -> cells
            List<List<List<string>>> tablesJson = new List<List<List<string>>>();

            foreach (AbsorbedTable absorbedTable in absorber.TableList)
            {
                List<List<string>> rowsJson = new List<List<string>>();

                foreach (AbsorbedRow absorbedRow in absorbedTable.RowList)
                {
                    List<string> cellsJson = new List<string>();

                    foreach (AbsorbedCell absorbedCell in absorbedRow.CellList)
                    {
                        // Concatenate all text fragments inside the cell
                        string cellText = string.Empty;
                        foreach (TextFragment fragment in absorbedCell.TextFragments)
                        {
                            cellText += fragment.Text;
                        }
                        cellsJson.Add(cellText?.Trim() ?? string.Empty);
                    }

                    rowsJson.Add(cellsJson);
                }

                tablesJson.Add(rowsJson);
            }

            // Serialize to pretty‑printed JSON
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(tablesJson, jsonOptions);

            // Save JSON to file
            File.WriteAllText(jsonOutput, jsonString);
            Console.WriteLine($"Extracted {tablesJson.Count} tables to '{jsonOutput}'.");
        }
    }
}
