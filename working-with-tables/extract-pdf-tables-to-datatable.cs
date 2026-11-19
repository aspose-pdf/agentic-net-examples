using System;
using System.IO;
using System.Data;
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
            // Create a TableAbsorber to extract tables from the document
            TableAbsorber absorber = new TableAbsorber();

            // Process all pages of the document
            absorber.Visit(doc);

            int tableIndex = 0;
            // Iterate over each extracted table via TableList
            foreach (var absorbedTable in absorber.TableList)
            {
                // Create a DataTable to hold the extracted data
                DataTable dataTable = new DataTable($"Table_{tableIndex}");

                // Determine the maximum column count across all rows
                int maxColumns = 0;
                foreach (var row in absorbedTable.RowList)
                {
                    if (row.CellList.Count > maxColumns)
                        maxColumns = row.CellList.Count;
                }

                // Add columns to the DataTable
                for (int col = 0; col < maxColumns; col++)
                {
                    dataTable.Columns.Add($"Column_{col + 1}", typeof(string));
                }

                // Populate the DataTable with cell text
                foreach (var row in absorbedTable.RowList)
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int col = 0; col < row.CellList.Count; col++)
                    {
                        var cell = row.CellList[col];
                        // Concatenate all TextFragments inside the cell
                        string cellText = string.Empty;
                        foreach (var fragment in cell.TextFragments)
                        {
                            cellText += fragment.Text;
                        }
                        dataRow[col] = cellText.Trim();
                    }
                    dataTable.Rows.Add(dataRow);
                }

                // Example output: print the extracted table to the console
                Console.WriteLine($"--- Extracted Table {tableIndex} ---");
                foreach (DataRow dr in dataTable.Rows)
                {
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        Console.Write($"{dr[i]}\t");
                    }
                    Console.WriteLine();
                }

                tableIndex++;
            }
        }
    }
}
