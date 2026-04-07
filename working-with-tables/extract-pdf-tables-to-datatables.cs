using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to find tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document
            absorber.Visit(doc);

            // List to hold the resulting DataTable objects
            List<DataTable> extractedTables = new List<DataTable>();
            int tableCounter = 0;

            // Iterate over each absorbed table
            foreach (var absorbedTable in absorber.TableList)
            {
                // Create a DataTable for this PDF table
                DataTable dt = new DataTable($"Table_{++tableCounter}");

                // First pass: ensure the DataTable has enough columns
                foreach (var row in absorbedTable.RowList)
                {
                    int cellIndex = 0;
                    foreach (var cell in row.CellList)
                    {
                        // Add a column if it does not yet exist
                        if (dt.Columns.Count <= cellIndex)
                        {
                            dt.Columns.Add($"Column_{cellIndex + 1}", typeof(string));
                        }
                        cellIndex++;
                    }
                }

                // Second pass: populate rows with cell text
                foreach (var row in absorbedTable.RowList)
                {
                    DataRow dataRow = dt.NewRow();
                    int cellIndex = 0;

                    foreach (var cell in row.CellList)
                    {
                        // Concatenate all text fragments inside the cell
                        string cellText = string.Empty;
                        foreach (var fragment in cell.TextFragments)
                        {
                            cellText += fragment.Text;
                        }

                        dataRow[cellIndex] = cellText;
                        cellIndex++;
                    }

                    dt.Rows.Add(dataRow);
                }

                extractedTables.Add(dt);
            }

            // Example output: write each DataTable to the console
            foreach (var dt in extractedTables)
            {
                Console.WriteLine($"--- {dt.TableName} ({dt.Rows.Count} rows, {dt.Columns.Count} columns) ---");

                // Header
                foreach (DataColumn col in dt.Columns)
                {
                    Console.Write($"{col.ColumnName}\t");
                }
                Console.WriteLine();

                // Rows
                foreach (DataRow row in dt.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        Console.Write($"{item}\t");
                    }
                    Console.WriteLine();
                }

                Console.WriteLine();
            }
        }
    }
}