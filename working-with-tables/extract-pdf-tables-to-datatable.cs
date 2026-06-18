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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to find tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document
            absorber.Visit(doc);

            // Iterate over each detected table
            for (int t = 0; t < absorber.TableList.Count; t++)
            {
                var absorbedTable = absorber.TableList[t];

                // Determine the maximum number of cells in any row (to define columns)
                int maxColumns = absorbedTable.RowList
                    .Max(r => r.CellList.Count);

                // Create a DataTable to hold the extracted data
                DataTable dataTable = new DataTable($"Table_{t + 1}");

                // Add columns to the DataTable
                for (int c = 0; c < maxColumns; c++)
                {
                    dataTable.Columns.Add($"Column{c + 1}", typeof(string));
                }

                // Populate rows
                foreach (var row in absorbedTable.RowList)
                {
                    DataRow dataRow = dataTable.NewRow();

                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        var cell = row.CellList[c];

                        // Concatenate all text fragments inside the cell
                        string cellText = string.Concat(
                            cell.TextFragments.Select(tf => tf.Text));

                        dataRow[c] = cellText;
                    }

                    dataTable.Rows.Add(dataRow);
                }

                // Example output: write table contents to console
                Console.WriteLine($"--- Extracted Table {t + 1} ---");
                foreach (DataRow dr in dataTable.Rows)
                {
                    Console.WriteLine(string.Join(" | ", dr.ItemArray));
                }
                Console.WriteLine();
            }
        }
    }
}