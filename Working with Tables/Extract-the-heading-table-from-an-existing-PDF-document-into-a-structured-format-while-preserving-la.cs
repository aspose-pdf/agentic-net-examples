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
        const string outputPath = "table.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document
            absorber.Visit(doc);

            // List to hold all extracted tables (each table is a list of rows, each row a list of cell texts)
            var extractedTables = new List<List<List<string>>>();

            foreach (var absorbedTable in absorber.TableList)
            {
                var tableData = new List<List<string>>();

                foreach (var row in absorbedTable.RowList)
                {
                    var rowData = new List<string>();

                    foreach (var cell in row.CellList)
                    {
                        // Combine all text fragments inside the cell
                        string cellText = string.Empty;
                        foreach (var fragment in cell.TextFragments)
                        {
                            cellText += fragment.Text;
                        }
                        rowData.Add(cellText);
                    }

                    tableData.Add(rowData);
                }

                extractedTables.Add(tableData);
            }

            // Serialize the hierarchical structure to JSON (preserves layout)
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(extractedTables, jsonOptions);
            File.WriteAllText(outputPath, json);
        }

        Console.WriteLine($"Table data extracted to '{outputPath}'.");
    }
}