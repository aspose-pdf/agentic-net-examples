using System;
using System.IO;
using System.Data;
using System.Linq;
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
            int tableIndex = 0;
            bool anyTableFound = false;

            // Iterate through each page because TableAbsorber works per‑page in the current API
            foreach (Page page in doc.Pages)
            {
                // Create a TableAbsorber and visit the current page
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(page);

                // Process each extracted table
                foreach (var tableInfo in absorber.TableList)
                {
                    anyTableFound = true;
                    // Create a DataTable to hold the extracted data
                    DataTable dt = new DataTable($"Table_{tableIndex}");

                    // Determine the maximum number of columns in this table
                    int maxColumns = tableInfo.RowList.Count > 0
                        ? tableInfo.RowList.Max(r => r.CellList.Count)
                        : 0;

                    // Add columns to the DataTable
                    for (int col = 0; col < maxColumns; col++)
                    {
                        dt.Columns.Add($"Column{col + 1}", typeof(string));
                    }

                    // Populate the DataTable with cell text values
                    foreach (var rowInfo in tableInfo.RowList)
                    {
                        DataRow dataRow = dt.NewRow();
                        for (int col = 0; col < rowInfo.CellList.Count; col++)
                        {
                            var cell = rowInfo.CellList[col];
                            // Concatenate all text fragments that belong to the cell
                            string cellText = string.Join(" ", cell.TextFragments.Select(tf => tf.Text));
                            dataRow[col] = cellText;
                        }
                        dt.Rows.Add(dataRow);
                    }

                    // Output basic information about the extracted table
                    Console.WriteLine($"Extracted Table {tableIndex}: {dt.Rows.Count} rows, {dt.Columns.Count} columns.");

                    // Optionally, print the table contents
                    foreach (DataRow dr in dt.Rows)
                    {
                        Console.WriteLine(string.Join("\t", dr.ItemArray));
                    }

                    tableIndex++;
                }
            }

            if (!anyTableFound)
            {
                Console.WriteLine("No tables found in the document.");
            }
        }
    }
}
